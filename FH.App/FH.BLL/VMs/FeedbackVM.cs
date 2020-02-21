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
        public int Stars { get; set; }
        public string Comment { get; set; } 
        public UserTabVM User { get; set; }
        public List<Icon> Photos { get; set; } = new List<Icon>();

        public FeedbackVM(Feedback m)
        {
            Stars = m.Stars;
            Comment = m.Comment;
            User = new UserTabVM(m.User);
            if (m.Photos != null && m.Photos.Any())
            {
                Photos = m.Photos?.Select(e => new Icon(e.Id, $"{e.Path}{e.Name}{e.Extension}")).ToList();
            } 
        }
    }
}
