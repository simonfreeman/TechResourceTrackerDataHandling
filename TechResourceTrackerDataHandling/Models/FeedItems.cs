using System;
using System.Collections.Generic;

namespace TechResourceTrackerDataHandling.Models
{
    public partial class FeedItems
    {
        public int Id { get; set; }
        public int FeedId { get; set; }
        public string Title { get; set; }
        public string ItemContent { get; set; }
        public DateTime DatePublished { get; set; }
        public bool Seen { get; set; }
        public string Url { get; set; }

        public Feed Feed { get; set; }
    }
}
