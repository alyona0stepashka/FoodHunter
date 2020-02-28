using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FH.Models.StaticModels;

namespace FH.Models.Models
{
    public class FileModel: BaseEntity
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public string Path { get; set; }

        [ForeignKey("Feedback")]
        public int? FeedbackId { get; set; }
        public virtual Feedback Feedback { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; } 

        //------------------------
        public virtual List<MenuItem> MenuItems { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual Company Company { get; set; }
    }
}
