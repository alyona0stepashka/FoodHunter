using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FH.Models.Models
{
    public class Manager: BaseEntity
    { 

        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        [ForeignKey("UserProfile")]
        public int? UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        //-------------------------
        public virtual List<Order> Orders { get; set; }
    }
}
