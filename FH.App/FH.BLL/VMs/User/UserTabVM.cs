using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class UserTabVM
    {
        public string Id { get; set; }
        public int UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public Icon Photo { get; set; } 

        public UserTabVM(UserProfile m)
        {
            Id = m.UserId;
            FirstName = m.FirstName;
            LastName = m.LastName;
            Sex = m.Sex.Value;
            Age = DateTime.Now.Year - m.DateBirth.Year;
            UserProfileId = m.Id;
            if (DateTime.Now.DayOfYear < m.DateBirth.DayOfYear)
            {
                Age--;
            }
            Photo = new Icon(m.Id, $"{m.File?.Path}{m.File?.Name}{m.File?.Extension}");
        }

        public UserTabVM()
        {
            
        }
    }
}
