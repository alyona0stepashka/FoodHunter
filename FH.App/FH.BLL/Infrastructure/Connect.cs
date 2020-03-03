using System;
using System.Collections.Generic;
using System.Text;

namespace FH.BLL.Infrastructure
{

    public class Connect
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        public List<int> OrderIds { get; set; } = new List<int>();
    }
}
