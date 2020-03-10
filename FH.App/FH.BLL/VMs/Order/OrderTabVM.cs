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
        public int ClientCount { get; set; } 
        public string LocationName { get; set; }
        public string ManagerName { get; set; }
        public int TableNumber { get; set; }
        public int ManagerId { get; set; }  

        public OrderTabVM(Order o)
        {
            Id = o.Id; 
            IsActive = o.IsActive;
            StartDate = o.StartDate;
            EndDate = o.EndDate;
            if (o.Table.LocationId != null) {LocationId = o.Table.LocationId.Value;}
            LocationName = o.Table.Location?.Name;  
            ClientCount = o.OrderUsers.Count;
            TableNumber = o.Table.Number;
            if (o.ManagerId != null)
            {
                ManagerId = o.ManagerId.Value;
                ManagerName = $"{o.Manager.UserProfile.FirstName} {o.Manager.UserProfile.LastName[0]}.";
            }
        }
    }
}
