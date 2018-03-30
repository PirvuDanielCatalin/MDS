using System;
using System.Collections.Generic;

namespace Jobs.Models
{
    public partial class Jobs
    {
        public Jobs()
        {
            Applications = new HashSet<Applications>();
            UsersInterests = new HashSet<UsersInterests>();
        }

        public int JobId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Type { get; set; }
        public string Interest { get; set; }
        public int? Experience { get; set; }
        public int? CompanyId { get; set; }

        public Companies Company { get; set; }
        public ICollection<Applications> Applications { get; set; }
        public ICollection<UsersInterests> UsersInterests { get; set; }
    }
}
