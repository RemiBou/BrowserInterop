namespace BrowserInterop
{
    public class DeviceMotionEvent
    {
        public DeviceMotionEventAcceleration Acceleration { get; set; }
        public DeviceMotionEventAcceleration AccelerationIncludingGravity { get; set; }
        public DeviceMotionEventRotationRate RotationRate { get; set; }
        public int Interval { get; set; }
    }
}