using System.Linq;

using Xunit;

namespace LEDRings.Tests
{
    public partial class LedRingTests
    {
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

        [Fact]
        public void CorrectMqttMessageForLedOnIsGenerated()
        {
            var expectedMesage = new MqttMessage("some/led7/rgb", "#FFFFFF");
            var actual = new LedControlMessage(7, LedValue.ON);

            Assert.Equal(actual, expectedMesage);
        }

        [Fact]
        public void CorrectMqttMessageForLedOffIsGenerated()
        {
            var expectedMesage = new MqttMessage("some/led4/rgb", "#000000");
            var actual = new LedControlMessage(4, LedValue.OFF);

            Assert.Equal(actual, expectedMesage);
        }


        [Theory]
        [InlineData(4, 0, new[] { LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF })]
        [InlineData(4, 25, new[] { LedValue.ON, LedValue.OFF, LedValue.OFF, LedValue.OFF })]
        [InlineData(4, 50, new[] { LedValue.ON, LedValue.ON, LedValue.OFF, LedValue.OFF })]
        [InlineData(4, 75, new[] { LedValue.ON, LedValue.ON, LedValue.ON, LedValue.OFF })]
        [InlineData(4, 100, new[] { LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON })]
        [InlineData(8, 0, new[] { LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF })]
        [InlineData(8, 24, new[] { LedValue.ON, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF })]
        [InlineData(8, 25, new[] { LedValue.ON, LedValue.ON, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF })]
        [InlineData(8, 50, new[] { LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.OFF, LedValue.OFF, LedValue.OFF, LedValue.OFF })]
        [InlineData(8, 74, new[] { LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.OFF, LedValue.OFF, LedValue.OFF })]
        [InlineData(8, 75, new[] { LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.OFF, LedValue.OFF })]
        [InlineData(8, 99, new[] { LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.OFF })]
        [InlineData(8, 100, new[] { LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON, LedValue.ON })]
        public void SendsCorrectMessagesForSingleLedRing(int ledCount, int profibility, LedValue[] expectedLedState)
        {
            var actualMessages = SetRing(profibility, ledCount);

            var expectedMessages = ConvertLedRingStateToMessages(expectedLedState);
            Assert.Equal(expectedMessages, actualMessages);
        }

        private static MqttMessage[] ConvertLedRingStateToMessages(LedValue[] ledRingState)
        {
            return ledRingState.Select((state, index) => new LedControlMessage(index, state)).ToArray();
        }

        private MqttMessage[] SetRing(int profibility, int totalLeds)
        {
            var sut = new LedRingService(totalLeds);
            return sut.SetRing(profibility);
        }
    }
}
