using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.Interfaces
{
    public interface IFileService
    {
        void IsValidFile(IFormFile file, int file_max_size_mb);
        Task<int> CreateFileDbAsync(IFormFile photo, int? feedbackId = null, string userId = null, int? locationId = null);
    }
}
