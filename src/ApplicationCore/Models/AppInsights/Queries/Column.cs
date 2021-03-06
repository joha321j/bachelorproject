// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

using System.Text.Json.Serialization;

#nullable disable

namespace ApplicationCore.Models.AppInsights.Queries
{
    /// <summary> A column in a table. </summary>
    public class Column
    {
        /// <summary> Initializes a new instance of Column. </summary>
        public Column()
        {
        }

        /// <summary> The name of this column. </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary> The data type of this column. </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
