using System;
using System.Collections.Generic;

namespace Jobs.Models
{
    public partial class Interests
    {
        public Interests()
        {
            UsersInterests = new HashSet<UsersInterests>();
        }

        public int InterestId { get; set; }
        public string Name { get; set; }

        public ICollection<UsersInterests> UsersInterests { get; set; }
    }
}
