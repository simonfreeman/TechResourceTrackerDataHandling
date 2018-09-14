using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechResourceTrackerDataHandling.Models
{
    public partial class MediaType
    {
        public MediaType()
        {
            Feed = new HashSet<Feed>();
        }

        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        public ICollection<Feed> Feed { get; set; }
    }
}
