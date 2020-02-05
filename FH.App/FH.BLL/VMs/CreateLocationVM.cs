using System;
using System.Collections.Generic;
using System.Text;

namespace FH.BLL.VMs
{
    public class CreateLocationVM
    {
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string Address { get; set; }
        public int CompanyId { get; set; }
    }
}
