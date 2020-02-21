using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class CreateMenuItemVM
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Info { get; set; } 
        public string Note { get; set; } 
        public decimal Price { get; set; }  
        public decimal? PriceWithSales { get; set; } 
        public bool IsActive { get; set; } 
        public int MenuId { get; set; } 
        public IFormFile Photo { get; set; } 
    }
}
