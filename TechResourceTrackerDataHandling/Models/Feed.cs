using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechResourceTrackerDataHandling.Models
{
    public partial class Feed
    {
        public Feed()
        {
            FeedItems = new HashSet<FeedItem>();
        }
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        public string Image { get; set; }

        public DateTime? LastUpdated { get; set; }

        [Required]
        public int MediaTypeId { get; set; }

        public MediaType MediaType { get; set; }

        public ICollection<FeedItem> FeedItems { get; set; }
    }
}
