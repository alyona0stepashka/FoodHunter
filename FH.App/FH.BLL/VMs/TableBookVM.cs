﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class TableBookVM
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }
        public DateTime BookTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsConfirm { get; set; }
        public TableTabVM Table { get; set; }
        public UserTabVM Client { get; set; }

        public TableBookVM(TableBook t)
        {
            Id = t.Id;
            StartTime = t.StartTime;
            EndTime = t.EndTime;
            BookTime = t.BookTime;
            IsActive = t.IsActive;
            IsConfirm = t.IsConfirm;
            if (t.Table != null) {Table = new TableTabVM(t.Table); }
            if (t.Client != null) { Client = new UserTabVM(t.Client); }

        }
    }
}
