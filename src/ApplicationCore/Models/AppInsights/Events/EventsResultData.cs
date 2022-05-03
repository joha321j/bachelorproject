// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace ApplicationCore.Models.AppInsights.Events
{
    /// <summary> Events query result data. </summary>
    public partial class EventsResultData
    {
        /// <summary> Initializes a new instance of EventsResultData. </summary>
        internal EventsResultData()
        {
        }

        /// <summary> Initializes a new instance of EventsResultData. </summary>
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
        internal EventsResultData(string id, EventType type, long? count, DateTimeOffset? timestamp, EventsResultDataCustomDimensions customDimensions, EventsResultDataCustomMeasurements customMeasurements, EventsOperationInfo operation, EventsSessionInfo session, EventsUserInfo user, EventsCloudInfo cloud, EventsAiInfo ai, EventsApplicationInfo application, EventsClientInfo client)
        {
            Id = id;
            Type = type;
            Count = count;
            Timestamp = timestamp;
            CustomDimensions = customDimensions;
            CustomMeasurements = customMeasurements;
            Operation = operation;
            Session = session;
            User = user;
            Cloud = cloud;
            Ai = ai;
            Application = application;
            Client = client;
        }

        /// <summary> The unique ID for this event. </summary>
        public string Id { get; }
        /// <summary> The type of event. </summary>
        internal EventType Type { get; set; }
        /// <summary> Count of the event. </summary>
        public long? Count { get; }
        /// <summary> Timestamp of the event. </summary>
        public DateTimeOffset? Timestamp { get; }
        /// <summary> Custom dimensions of the event. </summary>
        public EventsResultDataCustomDimensions CustomDimensions { get; }
        /// <summary> Custom measurements of the event. </summary>
        public EventsResultDataCustomMeasurements CustomMeasurements { get; }
        /// <summary> Operation info of the event. </summary>
        public EventsOperationInfo Operation { get; }
        /// <summary> Session info of the event. </summary>
        public EventsSessionInfo Session { get; }
        /// <summary> User info of the event. </summary>
        public EventsUserInfo User { get; }
        /// <summary> Cloud info of the event. </summary>
        public EventsCloudInfo Cloud { get; }
        /// <summary> AI info of the event. </summary>
        public EventsAiInfo Ai { get; }
        /// <summary> Application info of the event. </summary>
        public EventsApplicationInfo Application { get; }
        /// <summary> Client info of the event. </summary>
        public EventsClientInfo Client { get; }
    }
}
