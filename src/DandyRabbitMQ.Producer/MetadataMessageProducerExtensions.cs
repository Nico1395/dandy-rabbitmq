// using DandyRabbitMQ.Core.Messages;
// using DandyRabbitMQ.Core.Messages.Metadata;
//
// namespace DandyRabbitMQ.Producer;
//
// public static class MetadataMessageProducerExtensions
// {
//     public static Task<bool> ProduceAsync(this IMessageProducer producer, string exchange, Guid id, DateTime timestamp, object message, CancellationToken cancellationToken)
//     {
//         var routingKeys = MessageMetadataProvider.Get(message.GetType()).RoutingKeys;
//         if (routingKeys.Length == 0)
//             return Task.FromResult(false);
//
//         return producer.ProduceAsync(exchange, routingKeys, id, timestamp, message, cancellationToken);
//     }
//
//     public static Task<bool> ProduceAsync(this IMessageProducer producer, IEnumerable<string> routingKeys, Guid id, DateTime timestamp, object message, CancellationToken cancellationToken)
//     {
//         var exchange = MessageMetadataProvider.Get(message.GetType()).Exchange;
//         if (string.IsNullOrWhiteSpace(exchange))
//             return Task.FromResult(false);
//
//         return producer.ProduceAsync(exchange, routingKeys, id, timestamp, message, cancellationToken);
//     }
//
//     public static Task<bool> ProduceAsync(this IMessageProducer producer, Guid id, DateTime timestamp, object message, CancellationToken cancellationToken)
//     {
//         var metadata = MessageMetadataProvider.Get(message.GetType());
//         if (metadata.RoutingKeys.Length == 0 || string.IsNullOrWhiteSpace(metadata.Exchange))
//             return Task.FromResult(false);
//
//         return producer.ProduceAsync(metadata.Exchange, metadata.RoutingKeys, id, timestamp, message, cancellationToken);
//     }
//
//     public static Task<bool> ProduceAsync(this IMessageProducer producer, string exchange, IMessage message, CancellationToken cancellationToken)
//     {
//         var routingKeys = MessageMetadataProvider.Get(message.GetType()).RoutingKeys;
//         if (routingKeys.Length == 0)
//             return Task.FromResult(false);
//
//         return producer.ProduceAsync(exchange, routingKeys, message.Id, message.Timestamp, message, cancellationToken);
//     }
//
//     public static Task<bool> ProduceAsync(this IMessageProducer producer, IEnumerable<string> routingKeys, IMessage message, CancellationToken cancellationToken)
//     {
//         var exchange = MessageMetadataProvider.Get(message.GetType()).Exchange;
//         if (string.IsNullOrWhiteSpace(exchange))
//             return Task.FromResult(false);
//
//         return producer.ProduceAsync(exchange, routingKeys, message.Id, message.Timestamp, message, cancellationToken);
//     }
//
//     public static Task<bool> ProduceAsync(this IMessageProducer producer, IMessage message, CancellationToken cancellationToken)
//     {
//         var metadata = MessageMetadataProvider.Get(message.GetType());
//         if (metadata.RoutingKeys.Length == 0 || string.IsNullOrWhiteSpace(metadata.Exchange))
//             return Task.FromResult(false);
//
//         return producer.ProduceAsync(metadata.Exchange, metadata.RoutingKeys, message.Id, message.Timestamp, message, cancellationToken);
//     }
// }