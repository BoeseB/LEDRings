using System;
using System.Collections.Generic;
using System.Linq;

namespace LEDRings
{
    public class LedRingService
    {
        private const int MAX_PROFIBILITY = 100;
        private readonly int _ledCountRing1;
        private readonly IMqttClient _mqttClient;

        public LedRingService(IMqttClient mqttClient, int ledCountRing1)
        {
            _mqttClient = mqttClient;
            _ledCountRing1 = ledCountRing1;
        }

        public void SetRing(double profibility)
        {
            var ledStates = GetLedStates(profibility);
            var messages = GenerateMqttMessages(ledStates);
            SendMessages(messages);
        }

        private IEnumerable<LedValue> GetLedStates(double profibility)
        {
            var percentage = profibility / MAX_PROFIBILITY;
            var totalOnLeds = (int)Math.Floor(_ledCountRing1 * percentage);
            return Enumerable.Repeat(LedValue.ON, totalOnLeds)
                .Concat(Enumerable.Repeat(LedValue.OFF, _ledCountRing1 - totalOnLeds));
        }

        private IEnumerable<MqttMessage> GenerateMqttMessages(IEnumerable<LedValue> ledState)
        {
            return ledState.Select((state, index) => new LedControlMessage(index, state));
        }

        private void SendMessages(IEnumerable<MqttMessage> messages)
        {
            foreach (var message in messages)
            {
                _mqttClient.Send(message);
            }
        }

    }
}
