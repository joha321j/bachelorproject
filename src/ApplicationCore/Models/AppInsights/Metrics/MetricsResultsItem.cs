// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace ApplicationCore.Models.AppInsights.Metrics
{
    /// <summary> The MetricsResultsItem. </summary>
    public partial class MetricsResultsItem
    {

        public MetricsResultsItem()
        {
            
        }
        
        /// <summary> Initializes a new instance of MetricsResultsItem. </summary>
        /// <param name="id"> The specified ID for this metric. </param>
        /// <param name="status"> The HTTP status code of this metric query. </param>
        /// <param name="body"> The results of this metric query. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="id"/> or <paramref name="body"/> is null. </exception>
        public MetricsResultsItem(string id, int status, MetricsResult body)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            Id = id;
            Status = status;
            Body = body;
        }

        /// <summary> The specified ID for this metric. </summary>
        public string Id { get; }
        /// <summary> The HTTP status code of this metric query. </summary>
        public int Status { get; }
        /// <summary> The results of this metric query. </summary>
        public MetricsResult Body { get; }
    }
}