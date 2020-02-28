using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FH.Models.StaticModels;
using Microsoft.AspNetCore.Identity;

namespace FH.Models.Models
{
    public class Order: BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WelcomeCode { get; set; }

        public bool IsActive { get; set; }

        public string Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        public virtual Manager Manager { get; set; }

        [ForeignKey("Table")]
        public int? TableId { get; set; }
        public virtual Table Table { get; set; }

//        [ForeignKey("Location")]
//        public int? LocationId { get; set; }
//        public virtual Location Location { get; set; }

        //----------------------------- 
        public virtual List<OrderItem> OrderItems { get; set; }
        public virtual List<OrderUser> OrderUsers { get; set; }
        public virtual List<ManagerCall> ManagerCalls { get; set; }
    }
}
