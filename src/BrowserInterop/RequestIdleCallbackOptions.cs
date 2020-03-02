using System;

namespace BrowserInterop
{
    public class RequestIdleCallbackOptions
    {
        public RequestIdleCallbackOptions(TimeSpan timeout)
        {
            Timeout = timeout.TotalMilliseconds;

        }
        public RequestIdleCallbackOptions()
        {
        }
        public double Timeout { get; set; }
    }
}