// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace ApplicationCore.Models.AppInsights.Events
{
    /// <summary> Application info for an event result. </summary>
    public partial class EventsApplicationInfo
    {
        /// <summary> Initializes a new instance of EventsApplicationInfo. </summary>
        internal EventsApplicationInfo()
        {
        }

        /// <summary> Initializes a new instance of EventsApplicationInfo. </summary>
        /// <param name="version"> Version of the application. </param>
        internal EventsApplicationInfo(string version)
        {
            Version = version;
        }

        /// <summary> Version of the application. </summary>
        public string Version { get; }
    }
}
