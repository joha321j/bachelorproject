// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

using ApplicationCore.Models.AppInsights.Errors;

#nullable disable

namespace ApplicationCore.Models.AppInsights.Events
{
    /// <summary> An event query result. </summary>
    internal partial class EventsResult
    {
        /// <summary> Initializes a new instance of EventsResult. </summary>
        internal EventsResult()
        {
            AiMessages = new List<ErrorInfo>();
        }

        /// <summary> OData messages for this response. </summary>
        public IReadOnlyList<ErrorInfo> AiMessages { get; }
        /// <summary> Events query result data. </summary>
        public EventsResultData Value { get; }
    }
}
