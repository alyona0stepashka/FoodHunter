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
        public List<TableBookVM> TableBooks { get; set; } = new List<TableBookVM>(); 

        public TableTabVM(Table table)
        {
            IsHaveOrderNow = table.Orders?.Any(m => m.IsActive) ?? false;
            Id = table.Id;
            Number = table.Number;
            Info = table.Info;
            if (table.LocationId != null) LocationId = table.LocationId.Value; 
            if (table.TableBooks != null && table.TableBooks.Any())
            {
                TableBooks = table.TableBooks.Select(m=> new TableBookVM(m)).OrderBy(m=>m.StartTime).ToList(); 
            }
        }
    }
}
