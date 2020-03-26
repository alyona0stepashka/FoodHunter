using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FH.Models.Models
{
    public class ManagerCall: BaseEntity
    {
        public DateTime CallTime { get; set; }

        public bool IsActive { get; set; } 

        public string Comment { get; set; }

        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        //------------------------
    }
}
