// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace ApplicationCore.Models.AppInsights.Events
{
    /// <summary> The exception info. </summary>
    public partial class EventsExceptionInfo
    {
        /// <summary> Initializes a new instance of EventsExceptionInfo. </summary>
        internal EventsExceptionInfo()
        {
            Details = new List<EventsExceptionDetail>();
        }

        /// <summary> Initializes a new instance of EventsExceptionInfo. </summary>
        /// <param name="severityLevel"> The severity level of the exception. </param>
        /// <param name="problemId"> The problem ID of the exception. </param>
        /// <param name="handledAt"> Indicates where the exception was handled at. </param>
        /// <param name="assembly"> The assembly which threw the exception. </param>
        /// <param name="method"> The method that threw the exception. </param>
        /// <param name="message"> The message of the exception. </param>
        /// <param name="type"> The type of the exception. </param>
        /// <param name="outerType"> The outer type of the exception. </param>
        /// <param name="outerMethod"> The outer method of the exception. </param>
        /// <param name="outerAssembly"> The outer assembly of the exception. </param>
        /// <param name="outerMessage"> The outer message of the exception. </param>
        /// <param name="innermostType"> The inner most type of the exception. </param>
        /// <param name="innermostMessage"> The inner most message of the exception. </param>
        /// <param name="innermostMethod"> The inner most method of the exception. </param>
        /// <param name="innermostAssembly"> The inner most assembly of the exception. </param>
        /// <param name="details"> The details of the exception. </param>
        internal EventsExceptionInfo(int? severityLevel, string problemId, string handledAt, string assembly, string method, string message, string type, string outerType, string outerMethod, string outerAssembly, string outerMessage, string innermostType, string innermostMessage, string innermostMethod, string innermostAssembly, IReadOnlyList<EventsExceptionDetail> details)
        {
            SeverityLevel = severityLevel;
            ProblemId = problemId;
            HandledAt = handledAt;
            Assembly = assembly;
            Method = method;
            Message = message;
            Type = type;
            OuterType = outerType;
            OuterMethod = outerMethod;
            OuterAssembly = outerAssembly;
            OuterMessage = outerMessage;
            InnermostType = innermostType;
            InnermostMessage = innermostMessage;
            InnermostMethod = innermostMethod;
            InnermostAssembly = innermostAssembly;
            Details = details;
        }

        /// <summary> The severity level of the exception. </summary>
        public int? SeverityLevel { get; }
        /// <summary> The problem ID of the exception. </summary>
        public string ProblemId { get; }
        /// <summary> Indicates where the exception was handled at. </summary>
        public string HandledAt { get; }
        /// <summary> The assembly which threw the exception. </summary>
        public string Assembly { get; }
        /// <summary> The method that threw the exception. </summary>
        public string Method { get; }
        /// <summary> The message of the exception. </summary>
        public string Message { get; }
        /// <summary> The type of the exception. </summary>
        public string Type { get; }
        /// <summary> The outer type of the exception. </summary>
        public string OuterType { get; }
        /// <summary> The outer method of the exception. </summary>
        public string OuterMethod { get; }
        /// <summary> The outer assembly of the exception. </summary>
        public string OuterAssembly { get; }
        /// <summary> The outer message of the exception. </summary>
        public string OuterMessage { get; }
        /// <summary> The inner most type of the exception. </summary>
        public string InnermostType { get; }
        /// <summary> The inner most message of the exception. </summary>
        public string InnermostMessage { get; }
        /// <summary> The inner most method of the exception. </summary>
        public string InnermostMethod { get; }
        /// <summary> The inner most assembly of the exception. </summary>
        public string InnermostAssembly { get; }
        /// <summary> The details of the exception. </summary>
        public IReadOnlyList<EventsExceptionDetail> Details { get; }
    }
}
