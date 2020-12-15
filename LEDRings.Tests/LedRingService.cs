using System;
using System.Linq;

namespace LEDRings.Tests
{
    public class LedRingService
    {
        private const int MAX_PROFIBILITY = 100;
        private readonly int _ledCountRing1;

        public LedRingService(int ledCountRing1)
        {
            _ledCountRing1 = ledCountRing1;
        }
        public MqttMessage[] SetRing(int profibility)
        {
            var percentage = (double)profibility / (double)MAX_PROFIBILITY;
            var totalOnLeds = (int)Math.Floor(_ledCountRing1 * percentage);
            return Enumerable.Repeat(LedValue.ON, totalOnLeds)
                .Concat(Enumerable.Repeat(LedValue.OFF, _ledCountRing1 - totalOnLeds))
                .Select((state, index) => new LedControlMessage(index, state))
                .ToArray();
        }
    }
}
