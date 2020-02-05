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

        [ForeignKey("Location")]
        public int? LocationId { get; set; }

        [ForeignKey("Dealer")]
        public int? DealerId { get; set; }

        //---------------

        public virtual Feedback Feedback { get; set; }
        public virtual Location Location { get; set; }
        public virtual Dealer Dealer { get; set; }
    }
}
