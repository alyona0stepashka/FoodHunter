using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class OrderTabVM
    {
        public int Id { get; set; } 
        public bool IsActive { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime? EndDate { get; set; }  
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; } 

        public OrderTabVM(Order o)
        {
            Id = o.Id; 
            IsActive = o.IsActive;
            StartDate = o.StartDate;
            EndDate = o.EndDate; 
            LocationId = o.Table.LocationId.Value;
            LocationName = o.Table.Location?.Name;
            CompanyName = o.Table.Location?.Company?.Name;
            Address = o.Table.Location?.Address; 
        }
    }
}
