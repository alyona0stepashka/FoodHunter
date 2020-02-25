﻿using System;
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
        //public Location Location { get; set; } 
        public List<TableBookVM> TableBooks { get; set; } = new List<TableBookVM>();
        //public List<Order> Orders { get; set; }

        public TableTabVM(Table table)
        {
            Id = table.Id;
            Number = table.Number;
            Info = table.Info;
            LocationId = table.LocationId;
            if (table.TableBooks != null && table.TableBooks.Any())
            {
                TableBooks = table.TableBooks.Select(m=> new TableBookVM(m)).OrderBy(m=>m.StartTime).ToList();
            }
        }
    }
}
