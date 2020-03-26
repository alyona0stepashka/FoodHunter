using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.StaticModels;

namespace FH.BLL.VMs
{
    public class CompanySpecificationVM
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public CompanySpecificationVM(CompanySpecification s)
        {
            Id = s.Id;
            Value = s.Value;
        }

        public CompanySpecificationVM()
        {

        }
    }
}
