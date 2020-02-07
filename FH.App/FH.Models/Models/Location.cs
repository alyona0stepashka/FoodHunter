using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FH.Models.StaticModels;
using Microsoft.AspNetCore.Identity;

namespace FH.Models.Models
{
    public class Location: BaseEntity
    {
        public string Name { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public string Address { get; set; }

        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        public virtual IdentityUser Admin { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; } 
        public virtual Company Company { get; set; }

        //-----------------------
        public virtual List<Manager> Managers { get; set; }
        public virtual List<Subscription> Subscriptions { get; set; }
        public virtual List<Menu> Menus { get; set; }
    }
}
