using System;
using System.Collections.Generic;

namespace Jobs.Models
{
    public partial class Users
    {
        public Users()
        {
            Applications = new HashSet<Applications>();
            Cv = new HashSet<Cv>();
            UsersInterests = new HashSet<UsersInterests>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Phone { get; set; }
        public string Photo { get; set; }
        public string Location { get; set; }
        public int? Experience { get; set; }

        public ICollection<Applications> Applications { get; set; }
        public ICollection<Cv> Cv { get; set; }
        public ICollection<UsersInterests> UsersInterests { get; set; }
    }
}
