using Confluent.Kafka;
using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;

namespace JsonImporter.Queue
{
    class Producer
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void SendToKafka(List<Message> messages)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            Action<DeliveryReport<Null, byte[]>> handler = r =>
            {
                if(!r.Error.IsError)
                {
                    Console.WriteLine($"Delivered message to {r.TopicPartitionOffset}");
                    logger.Info($"Delivered message to {r.TopicPartitionOffset}");
                }
                else
                {
                    Console.WriteLine($"Delivery Error: {r.Error.Reason}");
                    logger.Error($"Delivery Error: {r.Error.Reason}");
                }
            };                

            try
            {
                using (var producer = new ProducerBuilder<Null, byte[]>(config).Build())
                {
                    foreach (Message message in messages)
                        producer.Produce("messages-topic", new Message<Null, byte[]> { Value = Serializer.SerializeMessage(message) }, handler);

                    producer.Flush(TimeSpan.FromSeconds(10));
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error while sending message to Kafka: " + ex);
            }
        }
    }
}
