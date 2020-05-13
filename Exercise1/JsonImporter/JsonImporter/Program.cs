using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using JsonImporter.Repositories;
using JsonImporter.Json;
using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonImporter
{
    [SimpleJob(launchCount: 5)]
    public class ParserBenchmark
    {
        private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static readonly string file = desktopPath + @"\message.json";
        private readonly JsonParser jsonParser;
        private readonly string jsonContent;

        public ParserBenchmark()
        {
            jsonParser = new JsonParser();
            jsonContent = Files.Read(file);
        }

        [Benchmark]
        public List<Message> Parser() => jsonParser.Parse(jsonContent);
    }

    [SimpleJob(launchCount: 1)]
    public class DbImporterBenchmark
    {
        private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static readonly string file = desktopPath + @"\message.json";
        private readonly JsonParser jsonParser;

        public DbImporterBenchmark()
        {
            jsonParser = new JsonParser();
        }

        [Benchmark]
        public int Importer() => MessageRepository.SaveMessages(jsonParser.Parse(Files.Read(file)));
    }

    public class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static readonly string file = desktopPath + @"\message.json";

        private static void GenerateJson()
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

        public static void Main(string[] args)
        {           
            if (!Directory.GetFiles(desktopPath).Any(f => f == file))
            {
                Console.WriteLine("Message.json is not exist. File will be created.");
                GenerateJson();
            }
            else
            {
                Console.Write("Message.json is already exist. Would you like to create a new one? Y/N: ");
                var result = Console.ReadLine();
                switch(result)
                {
                    case "Y":
                        GenerateJson();
                        break;
                    case "N":
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        Environment.Exit(1);
                        break;
                }
            }

            Console.Write("Would you like to run benchmark for JSON parser? Y/N: ");
            var jsonSelection = Console.ReadLine();
            switch (jsonSelection)
            {
                case "Y":
                    var summaryParser = BenchmarkRunner.Run<ParserBenchmark>();
                    break;
                case "N":
                    JsonParser jsonParser = new JsonParser();
                    List<Message> messages = jsonParser.Parse(Files.Read(file));

                    Console.Write("Would you like to run benchmark for database importer? Y/N: ");
                    var importerSelection = Console.ReadLine();
                    switch (importerSelection)
                    {
                        case "Y":
                            var summaryDbImporter = BenchmarkRunner.Run<DbImporterBenchmark>();
                            break;
                        case "N":
                            MessageRepository.SaveMessages(messages);
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please try again.");
                            Environment.Exit(1);
                            break;
                    }                    
                    
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    Environment.Exit(1);
                    break;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }    
}