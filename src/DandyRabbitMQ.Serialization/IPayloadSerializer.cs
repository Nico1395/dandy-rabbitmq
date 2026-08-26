namespace DandyRabbitMQ.Serialization;

public interface IPayloadSerializer
{
    string Serialize(object payload, Type inputType);
    object? Deserialize(string payload, Type returnType);
}