using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FH.Models.StaticModels;
using Microsoft.AspNetCore.Identity;

namespace FH.Models.Models
{
    public class Subscription: BaseEntity
    {
        public DateTime BuyDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [ForeignKey("SubscriptionType")]
        public int SubscriptionTypeId { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        //----------------------
    }
}
