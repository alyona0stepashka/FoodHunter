using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using FH.Models.Models;

namespace FH.BLL.VMs
{
    public class LocationTabVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string DealerName { get; set; }
        public string DealerPhoto { get; set; }
        public string Specification { get; set; }

        public LocationTabVM(Location location)
        {
            Id = location.Id;
            Name = location.Name;
            Address = location.Address;
            if (location.Dealer != null)
            {
                DealerName = location.Dealer.Name;
                DealerPhoto = $"{location.Dealer.File.Path}{location.Dealer.File.Name}{location.Dealer.File.Extension}";
                Specification = location.Dealer.Specification.Value;
            }
        }
    }
}
