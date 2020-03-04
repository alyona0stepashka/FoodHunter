using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FH.Models.EnumModels;
using FH.Models.Models;

namespace FH.BLL.VMs
{
    public class SearchTabVM
    {
        public int ObjId { get; set; }
        public string ObjType { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public double Rate { get; set; }
        public int RateCount { get; set; }
        public string PhotoPath { get; set; }

        public SearchTabVM(Location l)
        {
            ObjId = l.Id;
            ObjType = "location";
            Title = l.Name;
            Address = l.Address;
            Rate = 0;
            RateCount = 0;
            if (l.PhotoAlbum != null && l.PhotoAlbum.Any())
            {
                var photo = l.PhotoAlbum?.FirstOrDefault();
                PhotoPath = $"{photo?.Path}{photo?.Name}{photo?.Extension}";
            }
            if (l.Feedbacks != null && l.Feedbacks.Any())
            {
                RateCount = l.Feedbacks.Count();
                Rate = l.Feedbacks.Sum(e => e.Stars) / l.Feedbacks.Count;
            }
        }

        public SearchTabVM(Company l)
        {
            ObjId = l.Id;
            ObjType = "company";
            Title = l.Name;
            Address = l.Specification.Value;
            Rate = 0;
            var rateSum = 0;
            RateCount = 0;
            PhotoPath = $"{l.File?.Path}{l.File?.Name}{l.File?.Extension}"; 
            if (l.Locations != null && l.Locations.Any())
            {
                var locs = l.Locations;
                foreach (var loc in locs)
                {
                    if (loc.Feedbacks != null && loc.Feedbacks.Any())
                    {
                        RateCount += loc.Feedbacks.Count();
                        rateSum += loc.Feedbacks.Sum(e => e.Stars);
                    }
                }
                Rate = RateCount==0 ? 0 : rateSum / RateCount;
            }
        }
    }
}
