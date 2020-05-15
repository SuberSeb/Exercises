using Confluent.Kafka;
using JsonImporter.Tools;
using System;
using System.Threading;

namespace KafkaConsumer
{
    internal class Program
    {
        private static readonly string topicName = "messages-topic";

        private static void Main(string[] args)
        {
            Console.WriteLine("Starting Apache Kafka consumer...");
            Console.WriteLine();

            var config = new ConsumerConfig
            {
                GroupId = "messages-consumers",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, byte[]>(config).Build())
            {
                consumer.Subscribe(topicName);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var message = consumer.Consume(cts.Token);
                            Console.WriteLine($"{message.Message.Timestamp.UtcDateTime.ToLocalTime()}: " +
                                $"{Serializer.DeserializeMessage(message.Message.Value)}");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }
        }
    }
}