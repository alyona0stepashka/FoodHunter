using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.EnumModels;

namespace FH.BLL.VMs
{
    public class IconVM
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; } 

        public IconVM(int id, string val)
        {
            Id = id;
            Value = val;
        }
        public IconVM(Icon i)
        {
            if (i!=null) { 
            Id = i.Id;
            Value = i.Value;
            Description = i.Description;}
        }

        public IconVM()
        {

        }
    }
}
