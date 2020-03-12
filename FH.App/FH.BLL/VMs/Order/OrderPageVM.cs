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
        public List<OrderPageTabVM> Clients { get; set; } = new List<OrderPageTabVM>();
        public List<ManagerCallVM> ManagerCalls { get; set; }  = new List<ManagerCallVM>();
        public UserTabVM Manager { get; set; }
        public string ManagerName { get; set; }

        public OrderPageVM(Order o)
        {
            Id = o.Id;
            WelcomeCode = o.WelcomeCode;
            IsActive = o.IsActive;
            StartDate = o.StartDate;
            EndDate = o.EndDate;
            if (o.TableId != null) {TableId = o.TableId.Value;}
            TableNumber = o.Table?.Number.ToString();
            if (o.Table?.LocationId != null) {LocationId = (int) o.Table?.LocationId.Value;}

            if (o.Table != null && o.Table.Location != null)
            {
                LocationName = o.Table.Location?.Name;
                CompanyName = o.Table.Location?.Company?.Name;
                Address = o.Table.Location?.Address;
            }

            if (o.Manager != null)
            {
                if (o.Manager?.UserProfile != null)
                {
                    Manager = new UserTabVM(o.Manager?.UserProfile);
                    ManagerName = $"{o.Manager.UserProfile.FirstName} {o.Manager.UserProfile.LastName[0]}.";
                }
            }
            if (o.OrderUsers != null && o.OrderUsers.Any())
            {
                Clients = o.OrderUsers.Select(m => new OrderPageTabVM(m)).ToList();
            }
            if (o.ManagerCalls != null && o.ManagerCalls.Any())
            {
                ManagerCalls = o.ManagerCalls.Select(m => new ManagerCallVM(m)).OrderBy(m=>m.CallTime).Reverse().ToList();
            }
        }
    }
}
