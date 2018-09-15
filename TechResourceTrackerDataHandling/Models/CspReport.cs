using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechResourceTrackerDataHandling.Models
{
    public partial class CspReport
    {
        public CspReport()
        {

        }

        [Key]
        public int Id { get; set; }

        [JsonProperty(propertyName: "document-uri")]
        public string DocumentUri { get; set; }

        [JsonProperty(propertyName: "referrer")]
        public string Referrer { get; set; }

        [JsonProperty(propertyName: "blocked-uri")]
        public string BlockedUri { get; set; }

        [JsonProperty(propertyName: "violated-directive")]
        public string ViolatedDirective { get; set; }

        [JsonProperty(propertyName: "effective-directive")]
        public string EffectiveDirective { get; set; }

        [JsonProperty(propertyName: "original-policy")]
        public string OriginalPolicy { get; set; }

        [JsonProperty(propertyName: "disposition")]
        public string Disposition { get; set; }

        [JsonProperty(propertyName: "source-file")]
        public string SourceFile { get; set; }

        [JsonProperty(propertyName: "script-sample")]
        public string ScriptSample { get; set; }

        [JsonProperty(propertyName: "status-code")]
        public int StatusCode { get; set; }

        [JsonProperty(propertyName: "line-number")]
        public int LineNumber { get; set; }

        [JsonProperty(propertyName: "column-number")]
        public int ColumnNumber { get; set; }

        public DateTime DateViolated { get; set; }

    }
}
