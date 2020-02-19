using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;

namespace FH.BLL.VMs
{
    public class CreateMenuVM
    { 
        public int LocationId { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public int IconId { get; set; } 
    }
}
