using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.Models;

namespace FH.Models.StaticModels
{
    public class SubscriptionType : BaseEntity
    {
        public string Title { get; set; }

        public string Info { get; set; }

        public decimal PricePerMonth { get; set; }
        //------------------------

        public virtual List<Subscription> Subscriptions { get; set; }
    }
}
