// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace ApplicationCore.Models.AppInsights.Events
{
    /// <summary> The page view information. </summary>
    public partial class EventsPageViewInfo
    {
        /// <summary> Initializes a new instance of EventsPageViewInfo. </summary>
        internal EventsPageViewInfo()
        {
        }

        /// <summary> Initializes a new instance of EventsPageViewInfo. </summary>
        /// <param name="name"> The name of the page. </param>
        /// <param name="url"> The URL of the page. </param>
        /// <param name="duration"> The duration of the page view. </param>
        /// <param name="performanceBucket"> The performance bucket of the page view. </param>
        internal EventsPageViewInfo(string name, string url, string duration, string performanceBucket)
        {
            Name = name;
            Url = url;
            Duration = duration;
            PerformanceBucket = performanceBucket;
        }

        /// <summary> The name of the page. </summary>
        public string Name { get; }
        /// <summary> The URL of the page. </summary>
        public string Url { get; }
        /// <summary> The duration of the page view. </summary>
        public string Duration { get; }
        /// <summary> The performance bucket of the page view. </summary>
        public string PerformanceBucket { get; }
    }
}
