using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.Models;

namespace FH.Models.StaticModels
{
    public class CuisineUser: BaseEntity
    {
        public int CuisineId { get; set; }
        public virtual Cuisine Cuisine { get; set; }

        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        //--------------------

    }
}
