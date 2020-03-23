using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class ChartDataClientVM
    { 
        public int OrdersLastMonth { get; set; }
        public decimal PaymentLastMonth { get; set; }
        public int VisitedLocationsLastMonth { get; set; }
        public double AverageRate { get; set; }
        public int PerfectRateCount { get; set; }
        public int NormalRateCount { get; set; }
        public int BadRateCount { get; set; }
        public List<decimal> ClientPaymentActivity { get; set; } = new List<decimal>(); //for 12 month
    }
}
