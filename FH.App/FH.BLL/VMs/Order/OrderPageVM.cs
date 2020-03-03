using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class OrderPageVM
    {
        public int Id { get; set; }
        public Guid WelcomeCode { get; set; } 
        public bool IsActive { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime? EndDate { get; set; } 
        public int TableId { get; set; }
        public string TableNumber { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public List<UserTabVM> Users { get; set; } = new List<UserTabVM>();
        public UserTabVM Manager { get; set; }

        public OrderPageVM(Order o)
        {
            Id = o.Id;
            WelcomeCode = o.WelcomeCode;
            IsActive = o.IsActive;
            StartDate = o.StartDate;
            EndDate = o.EndDate;
            TableId = o.TableId.Value;
            TableNumber = o.Table?.Number.ToString();
            LocationId = o.Table.LocationId.Value;
            LocationName = o.Table.Location?.Name;
            CompanyName = o.Table.Location?.Company?.Name;
            Address = o.Table.Location?.Address;
            Manager = new UserTabVM(o.Manager?.UserProfile);
            if (o.OrderUsers != null && o.OrderUsers.Any())
            {
                Users = o.OrderUsers.Select(m => new UserTabVM(m.UserProfile)).ToList();
            }
        }
    }
}
