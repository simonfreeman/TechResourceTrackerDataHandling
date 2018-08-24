using System;
using System.Collections.Generic;

namespace TechResourceTrackerDataHandling.Models
{
    public partial class Feed
    {
        public Feed()
        {
            FeedItems = new HashSet<FeedItems>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int MediaTypeId { get; set; }

        public MediaType MediaType { get; set; }
        public ICollection<FeedItems> FeedItems { get; set; }
    }
}
