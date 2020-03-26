using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.Models;

namespace FH.Models.EnumModels
{
    public class Icon : BaseEntity
    {
        public string Value { get; set; }
        public string Description { get; set; }

        //-------------------
        public virtual List<Menu> Menus { get; set; }

        public Icon(int id, string val)
        {
            Id = id;
            Value = val;
        }

        public Icon()
        {
            
        }
    }
}
