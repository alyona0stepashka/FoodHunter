using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class CreateCompanyVM
    {
        public string ContactInfo { get; set; }

        public string Vk { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }

        public string Site { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int? SpecificationId { get; set; }

        public IFormFile LogoFile { get; set; }
    }
}
