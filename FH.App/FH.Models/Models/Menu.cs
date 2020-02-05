using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FH.Models.EnumModels;

namespace FH.Models.Models
{
    public class Menu: BaseEntity
    {
        public string Title { get; set; }

        public string Info { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }

        [ForeignKey("Icon")]
        public int IconId { get; set; }

        //-------------------------------
        public virtual List<MenuItem> MenuItems { get; set; }
        public virtual Location Location { get; set; }
        public virtual Icon Icon { get; set; }
    }
}
