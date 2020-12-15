using System;
using System.Linq;

using Xunit;

namespace LEDRings.Tests
{
    public class LedRingTests
    {
        private const string TOPIC_TEMPLATE = "some/led{0}/rgb";
        private const string LED_OFF = "#000000";
        private const string LED_ON = "#FFFFFF";

        private const int MAX_PROFIBILITY = 100;

        // 8 LED Ring
        // Prozent = Anzahl LEDS ON
        //    0 = 0
        //1-12  = 1
        //13-25 = 2
        //26-37 = 4
        //38-50 = 5
        //51-75 = 6
        //76-87 = 7
        //88-100 = 8

        [Theory]
        [InlineData(4, 0, new[] { LED_OFF, LED_OFF, LED_OFF, LED_OFF })]
        [InlineData(4, 25, new[] { LED_ON, LED_OFF, LED_OFF, LED_OFF })]
        [InlineData(4, 50, new[] { LED_ON, LED_ON, LED_OFF, LED_OFF })]
        [InlineData(4, 75, new[] { LED_ON, LED_ON, LED_ON, LED_OFF })]
        [InlineData(4, 100, new[] { LED_ON, LED_ON, LED_ON, LED_ON })]
        [InlineData(8, 0, new[] { LED_OFF, LED_OFF, LED_OFF, LED_OFF, LED_OFF, LED_OFF, LED_OFF, LED_OFF })]
        [InlineData(8, 24, new[] { LED_ON, LED_OFF, LED_OFF, LED_OFF, LED_OFF, LED_OFF, LED_OFF, LED_OFF })]
        [InlineData(8, 25, new[] { LED_ON, LED_ON, LED_OFF, LED_OFF, LED_OFF, LED_OFF, LED_OFF, LED_OFF })]
        [InlineData(8, 50, new[] { LED_ON, LED_ON, LED_ON, LED_ON, LED_OFF, LED_OFF, LED_OFF, LED_OFF })]
        [InlineData(8, 74, new[] { LED_ON, LED_ON, LED_ON, LED_ON, LED_ON, LED_OFF, LED_OFF, LED_OFF })]
        [InlineData(8, 75, new[] { LED_ON, LED_ON, LED_ON, LED_ON, LED_ON, LED_ON, LED_OFF, LED_OFF })]
        [InlineData(8, 99, new[] { LED_ON, LED_ON, LED_ON, LED_ON, LED_ON, LED_ON, LED_ON, LED_OFF })]
        [InlineData(8, 100, new[] { LED_ON, LED_ON, LED_ON, LED_ON, LED_ON, LED_ON, LED_ON, LED_ON })]
        public void SendsCorrectMessagesForSingleLedRing(int ledCount, int profibility, string[] expectedLedState)
        {
            var actualMessages = SetRing(profibility, ledCount);

            var expectedMessages = ConvertLedRingStateToMessages(expectedLedState);
            Assert.Equal(expectedMessages, actualMessages);
        }

        private static MqttMessage[] ConvertLedRingStateToMessages(string[] ledRingState)
        {
            return ledRingState.Select((state, index) => GetMessage(index, state)).ToArray();
        }

        private MqttMessage[] SetRing(int profibility, int totalLeds)
        {
            var percentage = (double)profibility / (double)MAX_PROFIBILITY;
            var totalOnLeds = (int)Math.Floor(totalLeds * percentage);
            return Enumerable.Repeat(LED_ON, totalOnLeds)
                .Concat(Enumerable.Repeat(LED_OFF, totalLeds - totalOnLeds))
                .Select((state, index) => GetMessage(index, state))
                .ToArray();
        }

        public static MqttMessage GetMessage(int ledNumber, string payload) => new MqttMessage(GetTopic(ledNumber), payload);

        private static string GetTopic(int ledNumber) => string.Format(TOPIC_TEMPLATE, ledNumber);


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
}
