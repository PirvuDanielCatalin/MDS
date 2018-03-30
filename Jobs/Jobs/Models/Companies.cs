using System;
using System.Collections.Generic;

namespace Jobs.Models
{
    public partial class Companies
    {
        public Companies()
        {
            Jobs = new HashSet<Jobs>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }

        public ICollection<Jobs> Jobs { get; set; }
    }
}
