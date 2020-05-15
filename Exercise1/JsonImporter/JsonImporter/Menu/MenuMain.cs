using BenchmarkDotNet.Running;
using JsonImporter.Json;
using JsonImporter.Models;
using JsonImporter.Queue;
using JsonImporter.Repositories;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static JsonImporter.Benchmark.PerformanceTest;

namespace JsonImporter.Menu
{
    internal class MenuMain
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
                Environment.Exit(1);
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
                var result = Console.ReadLine();
                switch (result)
                {
                    case "Y":
                        ShowGenerateJsonDialog();
                        break;

                    case "N":
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        Environment.Exit(1);
                        break;
                }
            }
        }

        public static List<Message> ShowJsonParserDialog()
        {
            List<Message> messages = new List<Message>();

            Console.Write("Would you like to run benchmark for JSON parser? Y/N: ");
            var selection = Console.ReadLine();
            switch (selection)
            {
                case "Y":
                    BenchmarkRunner.Run<ParserBenchmark>();
                    break;

                case "N":
                    JsonParser jsonParser = new JsonParser();
                    messages = jsonParser.Parse(Files.Read(file));
                    break;

                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    Environment.Exit(1);
                    break;
            }

            return messages;
        }

        public static void ShowDatabaseImportDialog(List<Message> messages)
        {
            Console.Write("Would you like to run benchmark for database importer? Y/N: ");
            var selection = Console.ReadLine();
            switch (selection)
            {
                case "Y":
                    BenchmarkRunner.Run<DbImporterBenchmark>();
                    break;

                case "N":
                    Console.Write("Chunk size: ");
                    int chunkSize = Convert.ToInt32(Console.ReadLine());
                    var chunks = messages.Chunk(chunkSize);

                    foreach (var chunk in chunks)
                    {
                        MessageRepository.SaveMessagesBulk(chunk.ToList());
                        Producer.SendToKafka(chunk.ToList());
                    }

                    break;

                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    Environment.Exit(1);
                    break;
            }
        }

        public static void ShowDeleteMessageFileDialog()
        {
            Console.Write("Would you like to delete generated message.json file? Y/N: ");
            var importerSelection = Console.ReadLine();
            switch (importerSelection)
            {
                case "Y":
                    File.Delete(file);
                    break;

                default:
                    Console.WriteLine("Exiting.");
                    Environment.Exit(1);
                    break;
            }
        }
    }
}