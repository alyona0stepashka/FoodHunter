using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class CreateTableVM
    {
        public int Id { get; set; }
        public int Number { get; set; } 
        public string Info { get; set; } 
        public int LocationId { get; set; }
    }
}
