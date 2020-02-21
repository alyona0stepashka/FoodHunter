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

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string Address { get; set; }

        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        public virtual IdentityUser Admin { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; } 
        public virtual Company Company { get; set; }

        [ForeignKey("TopFile")]
        public int? TopFileId { get; set; }
        public virtual FileModel TopFile { get; set; }

        //-----------------------
        public virtual List<Manager> Managers { get; set; }
        public virtual List<Subscription> Subscriptions { get; set; }
        public virtual List<Menu> Menus { get; set; }
        public virtual List<Table> Tables { get; set; }
        public virtual List<FileModel> PhotoAlbum { get; set; }
    }
}