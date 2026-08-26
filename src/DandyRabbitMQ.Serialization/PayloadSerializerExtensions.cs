namespace DandyRabbitMQ.Serialization;

public static class PayloadSerializerExtensions
{
    public static string Serialize(this IPayloadSerializer serializer, object payload)
    {
        return serializer.Serialize(payload, payload.GetType());
    }

    public static T? Deserialize<T>(this IPayloadSerializer serializer, string payload)
    {
        return serializer.Deserialize(payload, typeof(T)) is T casted ? casted : default;
    }
}