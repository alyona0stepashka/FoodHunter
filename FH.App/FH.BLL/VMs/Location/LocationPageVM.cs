using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;

namespace FH.BLL.VMs
{
    public class LocationPageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public decimal Rate { get; set; }
        public string Address { get; set; } 
        public List<IconVM> PhotoAlbum { get; set; } = new List<IconVM>();
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhoto { get; set; }
        public IconVM Specification { get; set; }
        public string Vk { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Site { get; set; }
        public List<FeedbackVM> Feedbacks { get; set; } = new List<FeedbackVM>();

        public LocationPageVM(Location location)
        {
            Rate = 0;
            if (location.PhotoAlbum != null && location.PhotoAlbum.Any())
            {
                PhotoAlbum = location.PhotoAlbum?.Select(m => new IconVM(m.Id, $"{m.Path}{m.Name}{m.Extension}")).ToList();
            }
            if (location.Feedbacks != null && location.Feedbacks.Any())
            {
                Feedbacks = location.Feedbacks?.Select(m => new FeedbackVM(m)).ToList(); 
                Rate = location.Feedbacks.Sum(e => e.Stars) / location.Feedbacks.Count;
            }
            Name = location.Name;
            Address = location.Address;
            Longitude = Convert.ToDecimal(location.Longitude.Replace('.', ','));
            Latitude = Convert.ToDecimal(location.Latitude.Replace('.', ','));
            if (location.Company != null)
            {
                Facebook = location.Company.Facebook;
                Vk = location.Company.Vk;
                Instagram = location.Company.Instagram;
                Site = location.Company.Site;
                Specification = new IconVM(location.Company.Specification.Id, location.Company.Specification.Value);
                CompanyId = location.Company.Id;
                CompanyName = location.Company.Name;
                CompanyPhoto =
                    $"{location.Company.File?.Path}{location.Company.File?.Name}{location.Company.File?.Extension}";
            }
        }
    }
}
