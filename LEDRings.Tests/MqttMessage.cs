using System;

namespace LEDRings.Tests
{
    public class MqttMessage
    {
        public string Topic { get; }
        public string Payload { get; }

        public MqttMessage(string topic, string payload)
        {
            Topic = topic;
            Payload = payload;
        }

        public override string ToString()
        {
            return $"Topic: {Topic}, Payload: {Payload}";
        }

        public override bool Equals(object obj)
        {
            return obj is MqttMessage message &&
                   Topic == message.Topic &&
                   Payload == message.Payload;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Topic, Payload);
        }
    }
}
