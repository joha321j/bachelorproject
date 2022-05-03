// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.ComponentModel;

namespace ApplicationCore.Models.AppInsights.Metrics
{
    /// <summary> The MetricsAggregation. </summary>
    public readonly partial struct MetricsAggregation : IEquatable<MetricsAggregation>
    {
        private readonly string _value;

        /// <summary> Determines if two <see cref="MetricsAggregation"/> values are the same. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public MetricsAggregation(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string MinValue = "min";
        private const string MaxValue = "max";
        private const string AvgValue = "avg";
        private const string SumValue = "sum";
        private const string CountValue = "count";
        private const string UniqueValue = "unique";

        /// <summary> min. </summary>
        public static MetricsAggregation Min { get; } = new MetricsAggregation(MinValue);
        /// <summary> max. </summary>
        public static MetricsAggregation Max { get; } = new MetricsAggregation(MaxValue);
        /// <summary> avg. </summary>
        public static MetricsAggregation Avg { get; } = new MetricsAggregation(AvgValue);
        /// <summary> sum. </summary>
        public static MetricsAggregation Sum { get; } = new MetricsAggregation(SumValue);
        /// <summary> count. </summary>
        public static MetricsAggregation Count { get; } = new MetricsAggregation(CountValue);
        /// <summary> unique. </summary>
        public static MetricsAggregation Unique { get; } = new MetricsAggregation(UniqueValue);
        /// <summary> Determines if two <see cref="MetricsAggregation"/> values are the same. </summary>
        public static bool operator ==(MetricsAggregation left, MetricsAggregation right) => left.Equals(right);
        /// <summary> Determines if two <see cref="MetricsAggregation"/> values are not the same. </summary>
        public static bool operator !=(MetricsAggregation left, MetricsAggregation right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="MetricsAggregation"/>. </summary>
        public static implicit operator MetricsAggregation(string value) => new MetricsAggregation(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is MetricsAggregation other && Equals(other);
        /// <inheritdoc />
        public bool Equals(MetricsAggregation other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
