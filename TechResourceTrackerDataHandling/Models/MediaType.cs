using System;
using System.Collections.Generic;

namespace TechResourceTrackerDataHandling.Models
{
    public partial class MediaType
    {
        public MediaType()
        {
            Feed = new HashSet<Feed>();
        }

        public int Id { get; set; }
        public string MediaType1 { get; set; }

        public ICollection<Feed> Feed { get; set; }
    }
}
