using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class TableTabVM
    {
        public int Id { get; set; }
        public int Number { get; set; } 
        public string Info { get; set; } 
        public int LocationId { get; set; }
        public bool IsHaveOrderNow { get; set; }
//        public string Status { get; set; }
        //public Location Location { get; set; } 
        public List<TableBookVM> TableBooks { get; set; } = new List<TableBookVM>();
        //public List<Order> Orders { get; set; }

        public TableTabVM(Table table)
        {
            IsHaveOrderNow = table.Orders.Any(m=>m.IsActive);
            Id = table.Id;
            Number = table.Number;
            Info = table.Info;
            LocationId = table.LocationId.Value;
//            Status = "";
            if (table.TableBooks != null && table.TableBooks.Any())
            {
                TableBooks = table.TableBooks.Select(m=> new TableBookVM(m)).OrderBy(m=>m.StartTime).ToList();
                //Status = table.TableBooks.Select(m=>m.)
            }
        }
    }
}
