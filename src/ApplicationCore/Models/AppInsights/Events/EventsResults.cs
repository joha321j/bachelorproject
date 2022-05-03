// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

using ApplicationCore.Models.AppInsights.Errors;

#nullable disable

namespace ApplicationCore.Models.AppInsights.Events
{
    /// <summary> An events query result. </summary>
    public partial class EventsResults
    {
        /// <summary> Initializes a new instance of EventsResults. </summary>
        internal EventsResults()
        {
            AiMessages = new List<ErrorInfo>();
            Value = new List<EventsResultData>();
        }

        /// <summary> Initializes a new instance of EventsResults. </summary>
        /// <param name="odataContext"> OData context metadata endpoint for this response. </param>
        /// <param name="aiMessages"> OData messages for this response. </param>
        /// <param name="value"> Contents of the events query result. </param>
        internal EventsResults(string odataContext, IReadOnlyList<ErrorInfo> aiMessages, IReadOnlyList<EventsResultData> value)
        {
            OdataContext = odataContext;
            AiMessages = aiMessages;
            Value = value;
        }

        /// <summary> OData context metadata endpoint for this response. </summary>
        public string OdataContext { get; }
        /// <summary> OData messages for this response. </summary>
        public IReadOnlyList<ErrorInfo> AiMessages { get; }
        /// <summary> Contents of the events query result. </summary>
        public IReadOnlyList<EventsResultData> Value { get; }
    }
}
