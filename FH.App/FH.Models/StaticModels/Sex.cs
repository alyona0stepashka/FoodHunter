using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.Models;

namespace FH.Models.EnumModels
{
    public class Sex : BaseEntity
    {
        public string Value { get; set; }
        //------------------------------
        public virtual List<UserProfile> Users { get; set; }
    }
}
