namespace BrowserInterop
{
    public class DeviceMotionEvent
    {
        public DeviceMotionEventAcceleration Acceleration { get; set; }
        public DeviceMotionEventAcceleration AccelerationIncludingGravity { get; set; }
        public DeviceMotionEventRotationRate RotationRate { get; set; }
        public int Interval { get; set; }

        public class DeviceMotionEventAcceleration
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }

        public class DeviceMotionEventRotationRate
        {
            public int Alpha { get; set; }
            public int Beta { get; set; }
            public int Gamma { get; set; }
        }
    }
}