using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class CreateOrderVM
    {
        public int? Id { get; set; } 
        public bool IsActive { get; set; } 
        public DateTime StartDate { get; set; } = DateTime.Now;
        //public DateTime EndDate { get; set; } 
        public int TableId { get; set; } 
        //public int LocationId { get; set; } 
    }
}
