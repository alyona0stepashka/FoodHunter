using System;
using System.Collections.Generic;
using System.Text;

namespace FH.BLL.Infrastructure
{

    public class OrderConnect
    {
        public int OrderId { get; set; }
        public string ManagerUserId { get; set; }
        public int ManagerConnectionId { get; set; }
        public List<Connect> Clients { get; set; } = new List<Connect>();
    }
}
