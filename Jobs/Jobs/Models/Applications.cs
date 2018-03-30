using System;
using System.Collections.Generic;

namespace Jobs.Models
{
    public partial class Applications
    {
        public int ApplicationId { get; set; }
        public int? JobId { get; set; }
        public int? UserId { get; set; }

        public Jobs Job { get; set; }
        public Users User { get; set; }
    }
}
