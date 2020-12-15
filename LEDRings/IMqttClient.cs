namespace LEDRings
{
    public interface IMqttClient
    {
        void Send(MqttMessage message);
    }
}