using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.DAL.EF.Interfaces;
using FH.Models.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.Services
{
    public class FileService: IFileService
    {
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        private IUnitOfWork _db { get; set; }
        private readonly IHostingEnvironment _appEnvironment;
        public FileService(IUnitOfWork uow,
            IHostingEnvironment appEnvironment)
        {
            _db = uow;
            _appEnvironment = appEnvironment;
        }

        public void IsValidFile(IFormFile file, int file_max_size_mb)
        {
            try
            {
                if (file.Length == 0)
                {
                    throw new Exception("Empty File");
                }
                if (file.Length > file_max_size_mb * 1024 * 1024)
                {
                    throw new Exception("Max file size exceeded.");
                }
                if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(file.FileName).ToLower()))
                {
                    throw new Exception("Invalid file type.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> CreateFileDbAsync(IFormFile photo, int? feedbackId = null, string userId = null, int? locationId = null, int? CompanyId = null)
        {
            try
            {
                IsValidFile(photo, 150);
                var file = new FileModel()
                {
                    Name = Guid.NewGuid().ToString(),
                    Extension = Path.GetExtension(photo.FileName),

                };
                if (feedbackId != null)
                {
                    file.FeedbackId = feedbackId;
                    file.Path = $"/Images/Feedbacks/{feedbackId}/";
                }
                if (userId != null)
                {
                    file.Path = $"/Images/Users/{userId}/";
                }
                if (locationId != null)
                {
                    file.LocationId = locationId;
                    file.Path = $"/Images/Locations/{locationId}/";
                }
                if (CompanyId != null)
                {
                    file.Path = $"/Images/Companys/{CompanyId}/";
                }
                await _db.FileModels.CreateAsync(file);
                using (var fileStream = new FileStream($"{_appEnvironment.WebRootPath}{file.Path}{file.Name}{file.Extension}", FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                return file.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
