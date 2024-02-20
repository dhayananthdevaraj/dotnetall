using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Container
    {
        public long ContainerId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int Capacity { get; set; }  
        public string Location { get; set; } 
        public double Weight { get; set; } 
        public string Owner { get; set; } 
        public DateTime CreationDate { get; set; }

        [JsonIgnore]
         public Assignment? Assignment { get; set; }

    }
}