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
        public string FullName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public IconVM Photo { get; set; } 

        public UserTabVM(UserProfile m, Sex s, FileModel f)
        {
            Id = m.UserId;
            FirstName = m.FirstName;
            LastName = m.LastName;
            FullName = $"{m.FirstName} {m.LastName[0]}.";
            if (s != null) Sex =s.Value;
            Age = DateTime.Now.Year - m.DateBirth.Year;
            UserProfileId = m.Id;
            if (DateTime.Now.DayOfYear < m.DateBirth.DayOfYear)
            {
                Age--;
            }

            if (f != null) Photo = new IconVM(m.Id, $"{f.Path}{f.Name}{f.Extension}");
        }

        public UserTabVM()
        {
            
        }
    }
}
