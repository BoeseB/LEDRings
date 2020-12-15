using System;
using System.Linq;

namespace LEDRings.Tests
{
    public class LedRingService
    {
        private const int MAX_PROFIBILITY = 100;

        public MqttMessage[] SetRing(int profibility, int totalLeds)
        {
            var percentage = (double)profibility / (double)MAX_PROFIBILITY;
            var totalOnLeds = (int)Math.Floor(totalLeds * percentage);
            return Enumerable.Repeat(LedValue.ON, totalOnLeds)
                .Concat(Enumerable.Repeat(LedValue.OFF, totalLeds - totalOnLeds))
                .Select((state, index) => new LedControlMessage(index, state))
                .ToArray();
        }
    }
}
