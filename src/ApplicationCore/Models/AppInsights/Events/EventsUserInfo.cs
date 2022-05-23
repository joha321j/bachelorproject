// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace ApplicationCore.Models.AppInsights.Events
{
    /// <summary> User info for an event result. </summary>
    public partial class EventsUserInfo
    {
        /// <summary> Initializes a new instance of EventsUserInfo. </summary>
        internal EventsUserInfo()
        {
        }

        /// <summary> Initializes a new instance of EventsUserInfo. </summary>
        /// <param name="id"> ID of the user. </param>
        /// <param name="accountId"> Account ID of the user. </param>
        /// <param name="authenticatedId"> Authenticated ID of the user. </param>
        internal EventsUserInfo(string id, string accountId, string authenticatedId)
        {
            Id = id;
            AccountId = accountId;
            AuthenticatedId = authenticatedId;
        }

        /// <summary> ID of the user. </summary>
        public string Id { get; }
        /// <summary> Account ID of the user. </summary>
        public string AccountId { get; }
        /// <summary> Authenticated ID of the user. </summary>
        public string AuthenticatedId { get; }
    }
}