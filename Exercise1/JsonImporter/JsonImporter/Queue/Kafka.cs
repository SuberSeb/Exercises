using Confluent.Kafka;
using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace JsonImporter.Queue
{
    internal class Kafka
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string topicName = "messages-topic";

        public static void SendMessages(List<Message> messages)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            Action<DeliveryReport<Null, byte[]>> handler = r =>
            {
                if (!r.Error.IsError)
                {
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
                var timer = new Stopwatch();
                using (var producer = new ProducerBuilder<Null, byte[]>(config).Build())
                {
                    timer.Start();
                    foreach (Message message in messages)
                        producer.Produce(topicName, new Message<Null, byte[]> { Value = Serializer.SerializeMessage(message) }, handler);

                    producer.Flush(TimeSpan.FromSeconds(10));
                    timer.Stop();
                }
                Console.WriteLine($"[Kafka SendMessage] Elapsed time: {timer.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while sending message to Kafka: " + ex);
                logger.Error("Error while sending message to Kafka: " + ex);
            }
        }
    }
}