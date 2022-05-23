// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections;

namespace ApplicationCore.Models.AppInsights.Metrics
{
    /// <summary> A metric result data. </summary>
    public class MetricsResultInfo
    {
        /// <summary> Initializes a new instance of MetricsResultInfo. </summary>
        public MetricsResultInfo()
        {
            Segments = new List<MetricsSegmentInfo>();
            AdditionalProperties = new Dictionary<string, string>();
        }

        /// <summary> Initializes a new instance of MetricsResultInfo. </summary>
        /// <param name="start"> Start time of the metric. </param>
        /// <param name="end"> Start time of the metric. </param>
        /// <param name="interval"> The interval used to segment the metric data. </param>
        /// <param name="segments"> Segmented metric data (if segmented). </param>
        /// <param name="additionalProperties"> . </param>
        public MetricsResultInfo(
            string start,
            string end,
            TimeSpan? interval,
            IReadOnlyList<MetricsSegmentInfo> segments,
            IReadOnlyDictionary<string, string> additionalProperties)
        {
            Start = start;
            End = end;
            Interval = interval;
            Segments = segments;
            AdditionalProperties = additionalProperties;
        }

        /// <summary> Start time of the metric. </summary>
        public string Start { get; }
        
        /// <summary> Start time of the metric. </summary>
        public string End { get; }
        
        /// <summary> The interval used to segment the metric data. </summary>
        public TimeSpan? Interval { get; }
        
        /// <summary> Segmented metric data (if segmented). </summary>
        public IReadOnlyList<MetricsSegmentInfo> Segments { get; }
        
        internal IReadOnlyDictionary<string, string> AdditionalProperties { get; }

        // public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => AdditionalProperties.GetEnumerator();

        public bool TryGetValue(string key, out string value) => AdditionalProperties.TryGetValue(key, out value);
        
        public bool ContainsKey(string key) => AdditionalProperties.ContainsKey(key);
        
        public List<string> Keys => AdditionalProperties.Keys.ToList();
        
        public List<string> Values => AdditionalProperties.Values.ToList();
        
        public object this[string key] => AdditionalProperties[key];
    }
}
