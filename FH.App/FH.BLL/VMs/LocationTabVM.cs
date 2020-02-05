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
        public string CompanyName { get; set; }
        public string CompanyPhoto { get; set; }
        public string Specification { get; set; }

        public LocationTabVM(Location location)
        {
            Id = location.Id;
            Name = location.Name;
            Address = location.Address;
            if (location.Company != null)
            {
                CompanyName = location.Company.Name;
                CompanyPhoto = $"{location.Company.File.Path}{location.Company.File.Name}{location.Company.File.Extension}";
                Specification = location.Company.Specification.Value;
            }
        }
    }
}
