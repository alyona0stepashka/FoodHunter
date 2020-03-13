using System;
using System.Collections.Generic;
using System.Text;

namespace FH.BLL.VMs
{
    public class EditAccountInfoVM
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? SexId { get; set; }

        public List<int> CuisinePreference { get; set; } = new List<int>();

        public DateTime? DateBirth { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public EditAccountInfoVM()
        {

        }
    }
}
