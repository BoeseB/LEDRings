namespace LEDRings
{
    public class LedRingSettings
    {
        public int LedCount { get; }
        public LedRingDirection Direction { get; }

        public LedRingSettings(int ledCount, LedRingDirection direction)
        {
            LedCount = ledCount;
            Direction = direction;
        }
    }
}
