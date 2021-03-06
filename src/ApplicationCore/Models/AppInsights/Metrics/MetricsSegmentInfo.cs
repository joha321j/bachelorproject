// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections;

namespace ApplicationCore.Models.AppInsights.Metrics
{
    /// <summary> A metric segment. </summary>
    public partial class MetricsSegmentInfo
    {
        /// <summary> Initializes a new instance of MetricsSegmentInfo. </summary>
        public MetricsSegmentInfo()
        {
            Segments = new List<MetricsSegmentInfo>();
            AdditionalProperties = new Dictionary<string, string>();
        }

        /// <summary> Initializes a new instance of MetricsSegmentInfo. </summary>
        /// <param name="start"> Start time of the metric segment (only when an interval was specified). </param>
        /// <param name="end"> Start time of the metric segment (only when an interval was specified). </param>
        /// <param name="segments"> Segmented metric data (if further segmented). </param>
        /// <param name="additionalProperties"> . </param>
        public MetricsSegmentInfo(string start, string end, IReadOnlyList<MetricsSegmentInfo> segments, 
        IReadOnlyDictionary<string, string> additionalProperties)
        {
            Start = start;
            End = end;
            Segments = segments;
            AdditionalProperties = additionalProperties;
        }

        /// <summary> Start time of the metric segment (only when an interval was specified). </summary>
        public string Start { get; }
        
        /// <summary> Start time of the metric segment (only when an interval was specified). </summary>
        public string End { get; }
        
        /// <summary> Segmented metric data (if further segmented). </summary>
        public IReadOnlyList<MetricsSegmentInfo> Segments { get; }

        internal IReadOnlyDictionary<string, string> AdditionalProperties { get; }
        
        public bool TryGetValue(string key, out string value) => AdditionalProperties.TryGetValue(key, out value);
        
        public bool ContainsKey(string key) => AdditionalProperties.ContainsKey(key);
        
        public IEnumerable<string> Keys => AdditionalProperties.Keys;
        
        public IEnumerable<string> Values => AdditionalProperties.Values;

        public string this[string key]
        {
            get => AdditionalProperties[key];
        }
    }
}
