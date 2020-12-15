using System;
using System.Collections.Generic;
using System.Linq;

namespace LEDRings
{

    public class LedRingService
    {
        private const int MAX_PROFIBILITY = 100;

        private readonly IMqttClient _mqttClient;
        private readonly LedRingSettings _ringSettings;

        public LedRingService(IMqttClient mqttClient, LedRingSettings ringSettings)
        {
            _mqttClient = mqttClient;
            _ringSettings = ringSettings;
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
            var totalOnLeds = (int)Math.Floor(_ringSettings.LedCount * percentage);

            var state = Enumerable.Repeat(LedValue.ON, totalOnLeds)
                .Concat(Enumerable.Repeat(LedValue.OFF, _ringSettings.LedCount - totalOnLeds));

            return _ringSettings.Direction == LedRingDirection.Clockwise ? state : state.Reverse();
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
