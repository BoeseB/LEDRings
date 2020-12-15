using System.Collections.Generic;

namespace LEDRings.Tests
{
    internal class FakeMqttClient : IMqttClient
    {
        public MqttMessage[] SentMessages => _sentMessages.ToArray();

        private readonly List<MqttMessage> _sentMessages = new List<MqttMessage>();

        public void Send(MqttMessage message)
        {
            _sentMessages.Add(message);
        }
    }
}