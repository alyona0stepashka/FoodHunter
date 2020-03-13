using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FH.Models.EnumModels;

namespace FH.Models.Models
{
    public class Table: BaseEntity
    {
        public int Number { get; set; }

        public string Info { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        //-------------------------------
        public virtual List<TableBook> TableBooks { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
