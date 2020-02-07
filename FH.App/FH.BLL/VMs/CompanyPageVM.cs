﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.Models;

namespace FH.BLL.VMs
{
    public class CompanyPageVM
    {
        public string ContactInfo { get; set; }

        public string Vk { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }

        public string Site { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public string Specification { get; set; }

        public string LogoPath { get; set; }

        public List<LocationPageVM> Locations { get; set; }

        public CompanyPageVM(Company c)
        {
            ContactInfo = c.ContactInfo;
            Vk = c.Vk;
            Facebook = c.Facebook;
            Instagram = c.Instagram;
            Site = c.Site;
            Name = c.Name;
            Describe = c.Describe;
            Specification = c.Specification.Value;
            LogoPath = $"{c.File.Path}{c.File.Name}{c.File.Extension}";
            Locations = c.Locations.Select(m => new LocationPageVM(m)).ToList();
        }
    }
}
