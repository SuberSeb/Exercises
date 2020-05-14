using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonImporter.Queue
{
    class Producer
    {
        public static void SendToKafka()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            Action<DeliveryReport<Null, string>> handler = r =>
                Console.WriteLine(!r.Error.IsError
                ? $"Delivered message to {r.TopicPartitionOffset}"
                : $"Delivery Error: {r.Error.Reason}");

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var stringValue = "";
                for (int i = 0; i < 100; ++i)
                {
                    stringValue += $"Test message {i}" + Environment.NewLine;
                    producer.ProduceAsync("messages-topic", new Message<Null, string> { Value = stringValue });
                }

                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
