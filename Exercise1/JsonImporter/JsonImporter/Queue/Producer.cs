using Confluent.Kafka;
using JsonImporter.Models;
using System;
using System.Collections.Generic;

namespace JsonImporter.Queue
{
    class Producer
    {
        public static void SendToKafka(List<Message> messages)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            Action<DeliveryReport<Null, Message>> handler = r =>
                Console.WriteLine(!r.Error.IsError
                ? $"Delivered message to {r.TopicPartitionOffset}"
                : $"Delivery Error: {r.Error.Reason}");

            using (var producer = new ProducerBuilder<Null, Message>(config).Build())
            {
                foreach (Message message in messages)
                    producer.Produce("messages-topic", new Message<Null, Message> { Value = message }, handler);

                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
