using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Infrastructure;
using FH.BLL.VMs;

namespace FH.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<string> RegisterUserAsync(RegisterVM model, string url);
        Task<OperationDetails> ConfirmEmailAsync(string useId, string code);
        Task<object> LoginUserAsync(LoginVM model);
        Task EditAccountInfo(EditAccountInfoVM model);
        List<UserTabVM> GetLocationStaff(string useId);
        void DeleteUser(string useId);
        void Dispose();
    }
}
