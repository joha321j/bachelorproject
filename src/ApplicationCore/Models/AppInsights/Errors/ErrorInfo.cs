// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace ApplicationCore.Models.AppInsights.Errors
{
    /// <summary> The code and message for an error. </summary>
    public partial class ErrorInfo
    {
        /// <summary> Initializes a new instance of ErrorInfo. </summary>
        /// <param name="code"> A machine readable error code. </param>
        /// <param name="message"> A human readable error message. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="code"/> or <paramref name="message"/> is null. </exception>
        internal ErrorInfo(string code, string message)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Code = code;
            Message = message;
            Details = new List<ErrorDetail>();
        }

        /// <summary> Initializes a new instance of ErrorInfo. </summary>
        /// <param name="code"> A machine readable error code. </param>
        /// <param name="message"> A human readable error message. </param>
        /// <param name="details"> error details. </param>
        /// <param name="innererror"> Inner error details if they exist. </param>
        /// <param name="additionalProperties"> Any object. </param>
        internal ErrorInfo(string code, string message, IReadOnlyList<ErrorDetail> details, ErrorInfo innererror, object additionalProperties)
        {
            Code = code;
            Message = message;
            Details = details;
            Innererror = innererror;
            AdditionalProperties = additionalProperties;
        }

        /// <summary> A machine readable error code. </summary>
        public string Code { get; }
        /// <summary> A human readable error message. </summary>
        public string Message { get; }
        /// <summary> error details. </summary>
        public IReadOnlyList<ErrorDetail> Details { get; }
        /// <summary> Inner error details if they exist. </summary>
        public ErrorInfo Innererror { get; }
        /// <summary> Any object. </summary>
        public object AdditionalProperties { get; }
    }
}
