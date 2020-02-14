using System;

namespace BrowserInterop.Performance
{
    /// <summary>
    /// Encapsulates a single performance metric that is part of the performance timeline. A performance entry can be directly created by making a performance mark or measure (for example by calling the mark() method) at an explicit point in an application. Performance entries are also created in indirect ways such as loading a resource (such as an image).
    /// </summary>
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
        public decimal StartTime { get; set; }

        /// <summary>
        /// StartTime as TimeSpan
        /// </summary>
        /// <returns></returns>
        public TimeSpan StartTimeTimeSpan => StartTime.HighResolutionTimeStampToTimeSpan();

        /// <summary>
        /// A DOMHighResTimeStamp representing the time value of the duration of the performance event.
        /// </summary>
        /// <returns></returns>
        public decimal Duration { get; set; }

        /// <summary>
        /// Duration as TimeSpan
        /// </summary>
        /// <returns></returns>
        public TimeSpan DurationTimeSpan => Duration.HighResolutionTimeStampToTimeSpan();
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