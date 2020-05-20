using BenchmarkDotNet.Running;
using JsonImporter.Json;
using JsonImporter.Models;
using JsonImporter.Repositories;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static JsonImporter.Benchmark.PerformanceTest;

namespace JsonImporter.Menu
{
    internal class ConsoleMenu
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static readonly string file = desktopPath + @"\message.json";

        private static void ShowGenerateJsonDialog()
        {
            int numberOfMessages = 0;
            Console.Write("Enter a number of messages to generate (500 messages = 7.5 megabytes): ");
            try
            {
                numberOfMessages = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid number of messages.");
                logger.Error("Invalid number of messages: " + ex);
                ShowExitDialog();
            }

            JsonGenerator jsonGenerator = new JsonGenerator();
            string json = jsonGenerator.Generate(numberOfMessages);

            Files.Write(file, json);
        }

        public static void ShowMessageCreateDialog()
        {
            if (!Directory.GetFiles(desktopPath).Any(f => f == file))
            {
                Console.WriteLine("Message.json is not exist. File will be created.");
                ShowGenerateJsonDialog();
            }
            else
            {
                Console.Write("Message.json is already exist. Would you like to create a new one? Y/N: ");
                var result = Console.ReadLine().ToLower();
                switch (result)
                {
                    case "y":
                        ShowGenerateJsonDialog();
                        break;

                    case "n":
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        ShowExitDialog();
                        break;
                }
            }
        }

        public static List<Message> ShowJsonParserDialog()
        {
            JsonParser jsonParser = new JsonParser();
            List<Message> messages = jsonParser.Parse(Files.Read(file));

            return messages;
        }

        public static void ShowImporterDialog(List<Message> messages)
        {
            Console.Write("Chunk size: ");
            int chunkSize = Convert.ToInt32(Console.ReadLine());
            var chunks = messages.Chunk(chunkSize);

            foreach (var chunk in chunks)
            {
                MessageRepository.SaveMessagesBulk(chunk.ToList());
                //Kafka.SendMessages(chunk.ToList());
            }
        }

        public static void ShowBenchmarkDialog()
        {
            Console.WriteLine("Would you like to run benchmark?");
            Console.Write("Choose and type Parser/Database/Kafka: ");
            var selection = Console.ReadLine().ToLower();
            switch (selection)
            {
                case "parser":
                    BenchmarkRunner.Run<ParserBenchmark>();
                    ShowExitDialog();
                    break;

                case "database":
                    BenchmarkRunner.Run<DbImporterBenchmark>();
                    ShowExitDialog();
                    break;

                case "kafka":
                    BenchmarkRunner.Run<KafkaBenchmark>();
                    ShowExitDialog();
                    break;

                default:
                    break;
            }
        }

        public static void ShowDeleteMessageFileDialog()
        {
            Console.Write("Would you like to delete generated message.json file? Y/N: ");
            var importerSelection = Console.ReadLine().ToLower();
            switch (importerSelection)
            {
                case "y":
                    File.Delete(file);
                    break;

                case "n":
                    break;

                default:
                    ShowExitDialog();
                    break;
            }
        }

        private static void ShowExitDialog()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}