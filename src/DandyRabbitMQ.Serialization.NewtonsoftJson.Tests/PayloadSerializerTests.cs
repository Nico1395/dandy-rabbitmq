using DandyRabbitMQ.Serialization.NewtonsoftJson.Tests.Fixtures;
using DandyRabbitMQ.Serialization.NewtonsoftJson.Tests.Mocks;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson.Tests;

public class PayloadSerializerTests(DefaultFixture fixture) : IClassFixture<DefaultFixture>
{
    [Fact]
    public void Serialize_ReturnsJsonString()
    {
        var serializer = fixture.GetPayloadSerializer();
        var json = serializer.Serialize(MessageMock.New(), typeof(MessageMock));

        Assert.NotNull(json);
    }

    [Fact]
    public void Deserialize_ReturnsObject()
    {
        var serializer = fixture.GetPayloadSerializer();
        var mock = MessageMock.New();
        var json = serializer.Serialize(mock, typeof(MessageMock));

        var deserialized = serializer.Deserialize(json, typeof(MessageMock));

        Assert.NotNull(deserialized);
        Assert.IsType<MessageMock>(deserialized);
        Assert.Equal(mock.Id, ((MessageMock)deserialized).Id);
        Assert.Equal(mock.Timestamp, ((MessageMock)deserialized).Timestamp);
        Assert.Equal(mock.Content, ((MessageMock)deserialized).Content);
    }
}