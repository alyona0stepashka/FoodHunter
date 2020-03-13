using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using FH.Models.Models;
using FH.Models.StaticModels;

namespace FH.Models.Models
{
    public class Feedback: BaseEntity
    {
        public int Stars { get; set; }

        public string Comment { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        [ForeignKey("MenuItem")]
        public int? MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }

        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        //--------------------------
        public virtual List<FileModel> Photos { get; set; }
        public virtual OrderUser OrderUser { get; set; }
    }
}
