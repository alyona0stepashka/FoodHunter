using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FH.BLL.Infrastructure;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using FH.DAL.EF.Interfaces;
using FH.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using FH.Models.StaticModels;

namespace FH.BLL.Services
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationSettings _applicationSettingsOption;
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;

        public AccountService(IUnitOfWork uow,
            UserManager<IdentityUser> userManager,
            IOptions<ApplicationSettings> applicationSettingsOption,
            IEmailService emailService,
            IFileService fileService)
        {
            _db = uow;
            _userManager = userManager;
            _applicationSettingsOption = applicationSettingsOption.Value;
            _emailService = emailService;
            _fileService = fileService;
        }

        public async Task<string> RegisterUserAsync(RegisterVM model, string url)
        {
            try

            {
                var user = new IdentityUser()
                {
                    Email = model.Email,
                    PasswordHash = model.Password,
                    UserName = model.Email,
                    EmailConfirmed = /*false*/ true
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Register error");
                }
                var fileId = await _fileService.CreateFileDbAsync(model.Photo, userId:user.Id);
                var userProfile = new UserProfile()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateBirth = model.DateBirth,
                    //DateLastOnline = DateTime.Now,
                    SexId = model.SexId,
                    FileId = fileId,
                    UserId = user.Id
                };
                await _db.UserProfiles.CreateAsync(userProfile);

                //await _chatService.SetLastOnlineAsync(user.Id);
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var encode = HttpUtility.UrlEncode(code);
                //var callbackUrl = new StringBuilder("https://")
                //    .AppendFormat(url)
                //    .AppendFormat("/api/account/email")
                //    .AppendFormat($"?user_id={user.Id}&code={encode}");

                //await _emailService.SendEmailAsync(user.Email, "Confirm your account",
                    //$"Confirm the registration by clicking on the link: <a href='{callbackUrl}'>link</a>");
                return user.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<OperationDetails> ConfirmEmailAsync(string user_id, string code)
        {
            try
            {
                var dbUser = await _userManager.FindByIdAsync(user_id);
                if (dbUser == null)
                {
                    throw new Exception("User not found");
                }
                var success = await _userManager.ConfirmEmailAsync(dbUser, code);
                return success.Succeeded ? new OperationDetails(true, "Success", "") : new OperationDetails(false, "Error", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<object> LoginUserAsync(LoginVM model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    if (!user.EmailConfirmed)
                    {
                        throw new Exception("Your Email is not confirmed");
                    }
                    //await _chatService.SetLastOnlineAsync(user.Id);
                    var options = new IdentityOptions();

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserID", user.Id),
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials =
                            new SigningCredentials(
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettingsOption.JwT_Secret)),
                                SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return token;
                }
                else
                    throw new Exception("User not found or invalid email/password");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task EditAccountInfo(EditAccountInfoVM model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (!String.IsNullOrEmpty(model.NewPassword) && !String.IsNullOrEmpty(model.OldPassword))
                {
                    if (user != null && await _userManager.CheckPasswordAsync(user, model.OldPassword))
                    {
                        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await _userManager.ResetPasswordAsync(user, code, model.NewPassword);

                        if (!result.Succeeded)
                        {
                            throw new Exception("Change password fail");
                        }
                    }
                    else
                    {
                        throw new Exception("User not found or invalid OldPassword");
                    }
                }

                var userProfile = _db.UserProfiles.GetAll().First(m => m.UserId == model.Id);
                if (userProfile == null)
                {
                    throw new Exception("User profile not found");
                }
                if (!String.IsNullOrEmpty(model.FirstName)) { userProfile.FirstName = model.FirstName; }
                if (!String.IsNullOrEmpty(model.LastName)) { userProfile.LastName = model.LastName; }
                if (model.SexId!=null) { userProfile.SexId = model.SexId; }
                if (model.DateBirth != null) { userProfile.DateBirth = model.DateBirth.Value; }

                var editResult = await _db.UserProfiles.UpdateAsync(userProfile);
                if (editResult==null)
                {
                    throw new Exception("Edit user info fail");
                }

                if (model.CuisinePreference != null)
                {
                    var dbCuisinePrefs = _db.CuisineUsers.GetAll().Where(m => m.UserProfileId == userProfile.Id);
                    foreach (var dbItem in dbCuisinePrefs)
                    {
                        if (!model.CuisinePreference.Select(m => m == dbItem.CuisineId).Any())
                        {
                           await _db.CuisineUsers.DeleteAsync(dbItem.Id);
                           model.CuisinePreference.Remove(dbItem.CuisineId);
                        }
                    }
                    foreach (var newItemId in model.CuisinePreference)
                    {
                        var bu = await _db.CuisineUsers.CreateAsync(new CuisineUser { CuisineId = newItemId, UserProfileId = userProfile.Id });
                    }
                }

                await _emailService.SendEmailAsync(user.Email, "Edit your account info",
                    $"Your Account Info has been updating.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
