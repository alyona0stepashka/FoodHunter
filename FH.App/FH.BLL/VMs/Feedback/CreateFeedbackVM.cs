using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class CreateFeedbackVM
    {
        public int Stars { get; set; }
        public string Comment { get; set; }
        public int? LocationId { get; set; }
        public int? MenuItemId { get; set; }
        public int UserProfileId { get; set; }
        //        public List<IFormFile> UploadPhotos { get; set; } 
        public IFormFile UploadPhoto { get; set; }
    }
}
