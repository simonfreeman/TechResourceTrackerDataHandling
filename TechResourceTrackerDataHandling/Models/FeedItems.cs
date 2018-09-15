using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechResourceTrackerDataHandling.Models
{
    public partial class FeedItem
    {
        public int Id { get; set; }

        [Required]
        public int FeedId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ItemContent { get; set; }

        [Required]
        public DateTime DatePublished { get; set; }

        public bool Seen { get; set; }

        [Required]
        public string Url { get; set; }

        public Feed Feed { get; set; }
    }
}
