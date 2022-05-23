// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace ApplicationCore.Models.AppInsights.Events
{
    /// <summary> A performance counter result. </summary>
    public partial class EventsPerformanceCounterResult : EventsResultData
    {
        /// <summary> Initializes a new instance of EventsPerformanceCounterResult. </summary>
        internal EventsPerformanceCounterResult()
        {
            Type = new EventType("performanceCounter");
        }

        /// <summary> Initializes a new instance of EventsPerformanceCounterResult. </summary>
        /// <param name="id"> The unique ID for this event. </param>
        /// <param name="type"> The type of event. </param>
        /// <param name="count"> Count of the event. </param>
        /// <param name="timestamp"> Timestamp of the event. </param>
        /// <param name="customDimensions"> Custom dimensions of the event. </param>
        /// <param name="customMeasurements"> Custom measurements of the event. </param>
        /// <param name="operation"> Operation info of the event. </param>
        /// <param name="session"> Session info of the event. </param>
        /// <param name="user"> User info of the event. </param>
        /// <param name="cloud"> Cloud info of the event. </param>
        /// <param name="ai"> AI info of the event. </param>
        /// <param name="application"> Application info of the event. </param>
        /// <param name="client"> Client info of the event. </param>
        /// <param name="performanceCounter"> The performance counter info. </param>
        internal EventsPerformanceCounterResult(string id, EventType type, long? count, DateTimeOffset? timestamp, EventsResultDataCustomDimensions customDimensions, EventsResultDataCustomMeasurements customMeasurements, EventsOperationInfo operation, EventsSessionInfo session, EventsUserInfo user, EventsCloudInfo cloud, EventsAiInfo ai, EventsApplicationInfo application, EventsClientInfo client, EventsPerformanceCounterInfo performanceCounter) : base(id, type, count, timestamp, customDimensions, customMeasurements, operation, session, user, cloud, ai, application, client)
        {
            PerformanceCounter = performanceCounter;
            Type = type;
        }

        /// <summary> The performance counter info. </summary>
        public EventsPerformanceCounterInfo PerformanceCounter { get; }
    }
}