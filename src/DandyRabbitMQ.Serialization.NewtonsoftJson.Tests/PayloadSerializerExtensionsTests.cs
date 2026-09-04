using DandyRabbitMQ.Serialization.NewtonsoftJson.Tests.Fixtures;
using DandyRabbitMQ.Serialization.NewtonsoftJson.Tests.Mocks;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson.Tests;

public class PayloadSerializerExtensionsTests(DefaultFixture fixture) : IClassFixture<DefaultFixture>
{
    [Fact]
    public void Serialize_WithoutType_ReturnsJsonString()
    {
        var serializer = fixture.GetPayloadSerializer();
        var json = serializer.Serialize(MessageMock.New());

        Assert.NotNull(json);
    }

    [Fact]
    public void Deserialize_WithGenericType_ReturnsObject()
    {
        var serializer = fixture.GetPayloadSerializer();
        var mock = MessageMock.New();
        var json = serializer.Serialize(mock);
        
        var deserialized = serializer.Deserialize<MessageMock>(json);

        Assert.NotNull(deserialized);
        Assert.Equal(mock.Id, deserialized.Id);
        Assert.Equal(mock.Timestamp, deserialized.Timestamp);
        Assert.Equal(mock.Content, deserialized.Content);
    }
}