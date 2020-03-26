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
        public List<ManagerCallVM> Calls { get; set; } = new List<ManagerCallVM>();

        public OrderTabVM(Order o, Location l, UserProfile p)
        {
            Id = o.Id; 
            IsActive = o.IsActive;
            if (o.OrderUsers != null)
            { 
                ClientCount = o.OrderUsers.Count;
            }
            
            StartDate = o.StartDate;
            EndDate = o.EndDate;
            if (l != null) LocationId = l.Id;

            if (o.Table != null)
            {
                if (l != null) LocationName = l.Name;
                TableNumber = o.Table.Number;
            }

            if (o.ManagerId != null)
            {
                ManagerId = o.ManagerId.Value;
                ManagerName = $"{p.FirstName} {p.LastName[0]}.";
            }

            if (o.ManagerCalls != null) Calls = o.ManagerCalls.Select(m => new ManagerCallVM(m)).ToList();
        }

        public OrderTabVM()
        {
            
        }
    }
}
