using System.Text.Json.Serialization;

namespace BrowserInterop.Performance
{
    /// <summary>
    /// Encapsulates a single performance metric that is part of the performance timeline. A performance entry can be directly created by making a performance mark or measure (for example by calling the mark() method) at an explicit point in an application. Performance entries are also created in indirect ways such as loading a resource (such as an image).
    /// </summary>
    [JsonConverter(typeof(PerformanceEntryConverter))]
    public class PerformanceEntry
    {
        /// <summary>
        /// A value that further specifies the value returned by the PerformanceEntry.entryType property. The value of both depends on the subtype.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// the type of performance metric such as, for example, "mark".
        /// </summary>
        /// <value></value>
        public string EntryType { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp representing the starting time for the performance metric.
        /// </summary>
        /// <value></value>
        public double StartTime { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp representing the time value of the duration of the performance event.
        /// </summary>
        /// <returns></returns>
        public double Duration { get; set; }
    }

    public class PerformanceMark : PerformanceEntry
    {
    }


    public class PerformanceMeasure : PerformanceEntry
    {
    }

    public class PerformanceFrameTiming : PerformanceEntry
    {
    }

    public class PerformancePaintTiming : PerformanceEntry
    {
    }

    public class PerformanceResourceTiming : PerformanceEntry
    {
        /// <summary>
        /// A string representing the type of resource that initiated the performance entry, as specified in PerformanceResourceTiming.initiatorType.
        /// </summary>
        /// <value></value>
        public string InitiatorType { get; set; }

        /// <summary>
        /// A string representing the network protocol used to fetch the resource, as identified by the ALPN Protocol ID (RFC7301).
        /// </summary>
        /// <value></value>
        public string NextHopProtocol { get; set; }

        /// <summary>
        /// Returns a DOMHighResTimeStamp immediately before dispatching the FetchEvent if a Service Worker thread is already running, or immediately before starting the Service Worker thread if it is not already running. If the resource is not intercepted by a Service Worker the property will always return 0.
        /// </summary>
        /// <value></value>
        public decimal WorkerStart { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp that represents the start time of the fetch which initiates the redirect.
        /// </summary>
        /// <value></value>
        public decimal RedirectStart { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp immediately after receiving the last byte of the response of the last redirect.
        /// </summary>
        /// <value></value>
        public decimal RedirectEnd { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp immediately before the browser starts to fetch the resource.
        /// </summary>
        /// <value></value>
        public decimal FetchStart { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp immediately before the browser starts the domain name lookup for the resource.
        /// </summary>
        /// <value></value>
        public decimal DomainLookupStart { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp representing the time immediately after the browser finishes the domain name lookup for the resource
        /// </summary>
        /// <value></value>
        public decimal DomainLookupEnd { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp immediately before the browser starts to establish the connection to the server to retrieve the resource.
        /// </summary>
        /// <value></value>
        public decimal ConnectStart { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp immediately after the browser finishes establishing the connection to the server to retrieve the resource.
        /// </summary>
        /// <value></value>
        public decimal ConnectEnd { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp immediately before the browser starts the handshake process to secure the current connection.
        /// </summary>
        /// <value></value>
        public decimal SecureConnectionStart { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp immediately before the browser starts requesting the resource from the server
        /// </summary>
        /// <value></value>
        public decimal RequestStart { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp immediately after the browser receives the first byte of the response from the server.
        /// </summary>
        /// <value></value>
        public decimal ResponseStart { get; set; }

        /// <summary>
        /// A DOMHighResTimeStamp immediately after the browser receives the last byte of the resource or immediately before the transport connection is closed, whichever comes first.
        /// </summary>
        /// <value></value>
        public decimal ResponseEnd { get; set; }

        /// <summary>
        ///  the size (in octets) of the fetched resource. The size includes the response header fields plus the response payload body.
        /// </summary>
        /// <value></value>
        public long TransferSize { get; set; }

        /// <summary>
        /// size (in octets) received from the fetch (HTTP or cache) of the message body, after removing any applied content-codings.
        /// </summary>
        /// <value></value>
        public long EncodedBodySize { get; set; }

        /// <summary>
        /// size (in octets) received from the fetch (HTTP or cache) of the message body, after removing any applied content-codings.
        /// </summary>
        /// <value></value>
        public long DecodedBodySize { get; set; }

        /// <summary>
        /// An array of PerformanceServerTiming entries containing server timing metrics.
        /// </summary>
        /// <value></value>
        public PerformanceServerTiming[] ServerTiming { get; set; }
    }

    /// <summary>
    /// Metrics that are sent with the response in the Server-Timing HTTP header.
    /// </summary>
    public class PerformanceServerTiming
    {
        /// <summary>
        /// server-specified metric description, or an empty string.
        /// </summary>
        /// <value></value>
        public string Description { get; set; }

        /// <summary>
        /// the server-specified metric duration, or value 0.0.
        /// </summary>
        /// <value></value>
        public decimal Duration { get; set; }

        /// <summary>
        /// server-specified metric name.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
    }

    public class PerformanceNavigationTiming : PerformanceEntry
    {
        public string InitiatorType { get; set; }
        public decimal DomComplete { get; set; }
        public decimal DomContentLoadedEventEnd { get; set; }
        public decimal DomContentLoadedEventStart { get; set; }
        public decimal Dominteractive { get; set; }
        public decimal LoadEventEnd { get; set; }
        public decimal LoadEventStart { get; set; }
        public int RedirectCount { get; set; }
        public decimal RequestStart { get; set; }
        public decimal ResponseStart { get; set; }
        public string Type { get; set; }
        public decimal UnloadEventEnd { get; set; }
        public decimal UnloadEventStart { get; set; }
    }
}