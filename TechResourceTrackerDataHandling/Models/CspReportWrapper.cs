using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechResourceTrackerDataHandling.Models
{
    public class CspReportWrapper
    {
        [JsonProperty(propertyName: "csp-report")]
        public CspReport Report { get; set; }
    }
}
