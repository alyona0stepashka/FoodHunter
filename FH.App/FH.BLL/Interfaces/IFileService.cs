using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.Models.EnumModels;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.Interfaces
{
    public interface IFileService
    {
        Task DeleteFile(int id);
        void IsValidFile(IFormFile file, int fileMaxSizeMb);
        Task<Icon> CreateFileDbAsync(IFormFile photo, int? feedbackId = null, string userId = null, int? locationId = null);
    }
}
