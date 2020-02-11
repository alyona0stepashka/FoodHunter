using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.Models;

namespace FH.BLL.VMs
{
    public class LocationPageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhoto { get; set; }
        public List<string> LocationPhotos { get; set; }
        public string Specification { get; set; }
        public string Vk { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Site { get; set; }
        public List<Menu> Menus { get; set; }

        public LocationPageVM(Location location)
        {
            Address = location.Address;
            Longitude = location.Longitude;
            Latitude = location.Latitude;
            if (location.Company != null)
            {
                Facebook = location.Company.Facebook;
                Vk = location.Company.Vk;
                Instagram = location.Company.Instagram;
                Site = location.Company.Site;
                Specification = location.Company.Specification.Value;
                CompanyName = location.Company.Name;
                CompanyPhoto = $"{location.Company.File.Path}{location.Company.File.Name}{location.Company.File.Extension}";
            }
        }
    }
}
