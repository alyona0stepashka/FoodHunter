using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.EnumModels;

namespace FH.BLL.VMs
{
    public class SexVM
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public SexVM(Sex s)
        {
            Id = s.Id;
            Value = s.Value;
        }

        public SexVM()
        {

        }
    }
}
