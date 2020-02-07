using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.StaticModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FH.Models.Models
{
    public class UserProfile: BaseEntity
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateBirth { get; set; }

        public DateTime DateLastOnline { get; set; }

        [ForeignKey("Sex")]
        public int? SexId { get; set; }
        public virtual Sex Sex { get; set; }


        [ForeignKey("File")]
        public int? FileId { get; set; }
        public virtual FileModel File { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        //---------------
        public virtual List<CuisineUser> CuisinePreference { get; set; }

    }
}
