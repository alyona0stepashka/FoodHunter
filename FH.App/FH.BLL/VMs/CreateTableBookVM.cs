using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class CreateTableBookVM
    {
        public int Id { get; set; }
        public DateTime StarTime { get; set; } 
        public DateTime EndTime { get; set; }
        public DateTime BookTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsConfirm { get; set; }
        public int TableId { get; set; } 
        public int ClientId { get; set; }  
    }
}
