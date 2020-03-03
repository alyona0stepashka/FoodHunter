using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using FH.Models.Models;

namespace FH.Models.StaticModels
{
    public class CompanySpecification: BaseEntity
    {
        public string Value { get; set; }
        public string AdditionalInfo { get; set; }

        //--------------------------
        public virtual List<Company> Companies{ get; set; }
    }
}
