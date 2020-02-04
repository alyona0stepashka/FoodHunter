﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FH.Models.Models
{
    public class MenuItem: BaseEntity
    {
        public string Title { get; set; }

        public string Info { get; set; }

        public string Note { get; set; }

        public decimal Price { get; set; }

        public decimal PriceWithSales { get; set; }

        [ForeignKey("Menu")]
        public int MenuId { get; set; }

        public bool IsActive { get; set; }

        //--------------------------
        public virtual Menu Menu { get; set; }
    }
}
