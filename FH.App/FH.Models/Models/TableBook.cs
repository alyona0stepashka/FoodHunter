using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FH.Models.EnumModels;
using Microsoft.AspNetCore.Identity;

namespace FH.Models.Models
{
    public class TableBook: BaseEntity
    {
        public DateTime StartTime { get; set; }

        public DateTime BookTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsConfirm { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual UserProfile Client { get; set; }

        //-------------------------------
        //public virtual List<TableBook> TableBooks { get; set; }
    }
}
