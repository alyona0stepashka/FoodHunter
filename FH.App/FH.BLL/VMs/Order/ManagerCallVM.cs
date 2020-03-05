using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class ManagerCallVM
    {
        public int? Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CallTime { get; set; } = DateTime.Now;
        public string Comment { get; set; }
        public int OrderId { get; set; } 

        public ManagerCallVM(ManagerCall m)
        {
            Id = m.Id; 
            IsActive = m.IsActive;
            CallTime = m.CallTime;
            Comment = m.Comment;
            OrderId = m.OrderId.Value;
        }

        public ManagerCallVM()
        {
            
        }
    }
}
