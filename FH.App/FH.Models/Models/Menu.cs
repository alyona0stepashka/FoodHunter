using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FH.Models.Models
{
    public class Menu: BaseEntity
    {
        public string Title { get; set; }

        public string Info { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }

        //-------------------------------
        public virtual List<MenuItem> MenuItems { get; set; }
        public virtual Location Location { get; set; }
    }
}
