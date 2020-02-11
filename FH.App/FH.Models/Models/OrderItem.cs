using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FH.Models.Models
{
    public class OrderItem: BaseEntity
    {
        public string Title { get; set; }

        public double Count { get; set; }

        public decimal PricePerItem { get; set; }

        public string Status { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual UserProfile User { get; set; }

        //------------------------
    }
}
