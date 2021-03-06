﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FH.Models.StaticModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FH.Models.Models
{
    public class Company: BaseEntity
    {
        public string ContactInfo { get; set; }

        public string Vk { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }

        public string Site { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        [ForeignKey("Specification")]
        public int? SpecificationId { get; set; }
        public virtual CompanySpecification Specification { get; set; }

        [ForeignKey("File")]
        public int? FileId { get; set; }
        public virtual FileModel File { get; set; }

        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        public virtual IdentityUser Admin { get; set; }

        //--------------------------
        public virtual List<Location> Locations { get; set; }

    }
}
