using System;
using System.Collections.Generic;

namespace Jobs.Models
{
    public partial class Cv
    {
        public int CvId { get; set; }
        public int? UserId { get; set; }
        public string CvPath { get; set; }

        public Users User { get; set; }
    }
}
