using System;
using System.Collections.Generic;
using System.Text;

namespace FH.Models.StaticModels
{
    public class Cuisine : BaseEntity
    {
        public string Value { get; set; }
        //---------------------
        public virtual List<CuisineUser> CuisineUsers { get; set; }
    }
}