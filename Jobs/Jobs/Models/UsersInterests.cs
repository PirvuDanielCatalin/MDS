using System;
using System.Collections.Generic;

namespace Jobs.Models
{
    public partial class UsersInterests
    {
        public int UsersInterestsId { get; set; }
        public int? UserId { get; set; }
        public int? JobId { get; set; }
        public int? InterestId { get; set; }

        public Interests Interest { get; set; }
        public Jobs Job { get; set; }
        public Users User { get; set; }
    }
}
