using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace FH.Models.Models
{
    public class Order: BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WelcomeCode { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }

        //-----------------------------
        public virtual List<OrderItem> OrderItems { get; set; }
        public virtual Location Location { get; set; }
    }
}
