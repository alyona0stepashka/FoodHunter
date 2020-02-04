using System;
using System.Collections.Generic;
using System.Text;

namespace FH.Models.StaticModels
{
    public class SubscriptionType : BaseEntity
    {
        public string Title { get; set; }

        public string Info { get; set; }

        public decimal PricePerMonth { get; set; }
    }
}
