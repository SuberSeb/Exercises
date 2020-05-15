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
            var config = new ConsumerConfig
            {
                GroupId = "messages-consumers",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            Console.Write("Would you like to see message content? Y/N: ");
            var selection = Console.ReadLine();
            var result = string.Empty;
            switch (selection)
            {
                case "Y":
                    result = selection;
                    break;

                case "N":
                    result = selection;
                    break;

                default:
                    Console.WriteLine("Invalid input. Messages content no will be shown.");
                    result = "N";
                    break;
            }
            Console.WriteLine();
            Console.WriteLine("Listening...");
            Console.WriteLine();

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
                            if (result == "Y")
                            {
                                Console.WriteLine($"{message.Message.Timestamp.UtcDateTime.ToLocalTime()}: " +
                                $"{Serializer.DeserializeMessage(message.Message.Value)}");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine($"{message.Message.Timestamp.UtcDateTime.ToLocalTime()}: message received.");
                            }
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