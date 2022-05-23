// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace ApplicationCore.Models.AppInsights.Queries
{
    /// <summary> A column in a table. </summary>
    public partial class Column
    {
        /// <summary> Initializes a new instance of Column. </summary>
        internal Column()
        {
        }

        /// <summary> Initializes a new instance of Column. </summary>
        /// <param name="name"> The name of this column. </param>
        /// <param name="type"> The data type of this column. </param>
        public Column(string name, string type)
        {
            Name = name;
            Type = type;
        }

        /// <summary> The name of this column. </summary>
        public string Name { get; }
        /// <summary> The data type of this column. </summary>
        public string Type { get; }
    }
}
