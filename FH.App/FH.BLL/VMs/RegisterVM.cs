using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{

    public class RegisterVM
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Range(typeof(DateTime), "1900-12-12", "2020-12-12",
            ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime DateBirth { get; set; }

        public int? SexId { get; set; }

        public IFormFile Photo { get; set; }
    }
}
