using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace FH.Models.StaticModels
{
    public class CompanySpecification: BaseEntity
    {
        public string Value { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
