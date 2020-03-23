using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class ChartDataManagerVM
    { 
        public int OrdersLastMonth { get; set; }
        public decimal PaymentLastMonth { get; set; }
        public int ClientsToday { get; set; }
        public double AverageRate { get; set; }
        public List<int> TableOccupancyYesterday { get; set; } = new List<int>(); //for 24 hours 
        public List<int> TableOccupancyToday { get; set; } = new List<int>(); //for 24 hours 
        public int PerfectRateCount { get; set; }
        public int NormalRateCount { get; set; }
        public int BadRateCount { get; set; }
        public List<decimal> ClientPaymentActivity { get; set; } = new List<decimal>(); //for 12 month
    }
}
