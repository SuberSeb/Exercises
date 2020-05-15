using Confluent.Kafka;
using JsonImporter.Tools;
using System;
using System.Threading;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Apache Kafka consumer...");

            var config = new ConsumerConfig
            {
                GroupId = "messages-consumers",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, byte[]>(config).Build())
            {
                consumer.Subscribe("messages-topic");

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = consumer.Consume(cts.Token);
                            Console.WriteLine($"Message recieved in: {cr.Message.Timestamp}");
                            Console.WriteLine(Serializer.DeserializeMessage(cr.Message.Value));
                            Console.WriteLine();
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
