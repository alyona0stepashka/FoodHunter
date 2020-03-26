using System;
using System.Collections.Generic;
using System.Text;

namespace FH.BLL.VMs
{
    public class CreateLocationVM
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Address { get; set; }
        public int CompanyId { get; set; }
    }
}
