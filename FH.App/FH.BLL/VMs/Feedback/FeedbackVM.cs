using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class FeedbackVM
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; } 
        public UserTabVM User { get; set; } = new UserTabVM();
        public List<IconVM> Photos { get; set; } = new List<IconVM>();

        public FeedbackVM(Feedback m)
        {
            if (m != null) { 
                Id = m.Id;
                Stars = m.Stars;
                Comment = m.Comment;
                if (m.UserProfile != null) 
                {
                    User = new UserTabVM(m.UserProfile);
                }
                if (m.Photos != null && m.Photos.Any())
                {
                    Photos = m.Photos?.Select(e => new IconVM(e.Id, $"{e.Path}{e.Name}{e.Extension}")).ToList();
                }
            }
        }
    }
}
