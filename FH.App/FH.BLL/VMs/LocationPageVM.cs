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
        public string DealerName { get; set; }
        public string DealerPhoto { get; set; }
        public string LocationPhoto { get; set; }
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
            if (location.Dealer != null)
            {
                Facebook = location.Dealer.Facebook;
                Vk = location.Dealer.Vk;
                Instagram = location.Dealer.Instagram;
                Site = location.Dealer.Site;
                Specification = location.Dealer.Specification.Value;
                DealerName = location.Dealer.Name;
                DealerPhoto = $"{location.Dealer.File.Path}{location.Dealer.File.Name}{location.Dealer.File.Extension}";
            }
        }
    }
}
