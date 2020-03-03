using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.Models;

namespace FH.Models.StaticModels
{
    public class OrderUser: BaseEntity
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        //--------------------

    }
}
