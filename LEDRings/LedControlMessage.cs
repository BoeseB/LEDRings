using System;

namespace LEDRings.Tests
{
    public class LedControlMessage : MqttMessage
    {
        private const string TOPIC_TEMPLATE = "some/led{0}/rgb";
        private const string LED_OFF = "#000000";
        private const string LED_ON = "#FFFFFF";

        public LedControlMessage(int index, LedValue value)
            : base(GetTopic(index), GetValue(value))
        {

        }

        private static string GetTopic(int index) => string.Format(TOPIC_TEMPLATE, index);
        private static string GetValue(LedValue value) => value switch
        {
            LedValue.OFF => LED_OFF,
            LedValue.ON => LED_ON,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null),
        };
    }

}
