// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections;

namespace ApplicationCore.Models.AppInsights.Events
{
    /// <summary> Custom measurements of the event. </summary>
    public partial class EventsResultDataCustomMeasurements : IReadOnlyDictionary<string, object>
    {
        /// <summary> Initializes a new instance of EventsResultDataCustomMeasurements. </summary>
        internal EventsResultDataCustomMeasurements()
        {
            AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary> Initializes a new instance of EventsResultDataCustomMeasurements. </summary>
        /// <param name="additionalProperties"> Any object. </param>
        /// <param name="additionalProperties"> . </param>
        internal EventsResultDataCustomMeasurements(IReadOnlyDictionary<string, object> additionalProperties)
        {
            AdditionalProperties = additionalProperties;
        }
        
        internal IReadOnlyDictionary<string, object> AdditionalProperties { get; }
        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => AdditionalProperties.GetEnumerator();
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => AdditionalProperties.GetEnumerator();
        /// <inheritdoc />
        public bool TryGetValue(string key, out object value) => AdditionalProperties.TryGetValue(key, out value);
        /// <inheritdoc />
        public bool ContainsKey(string key) => AdditionalProperties.ContainsKey(key);
        /// <inheritdoc />
        public IEnumerable<string> Keys => AdditionalProperties.Keys;
        /// <inheritdoc />
        public IEnumerable<object> Values => AdditionalProperties.Values;
        /// <inheritdoc cref="IReadOnlyCollection{T}.Count"/>
        int IReadOnlyCollection<KeyValuePair<string, object>>.Count => AdditionalProperties.Count;
        /// <inheritdoc />
        public object this[string key]
        {
            get => AdditionalProperties[key];
        }
    }
}
