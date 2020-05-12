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

    public class Program
    {
        private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static readonly string file = desktopPath + @"\message.json";

        private static void GenerateJson()
        {
            Console.Write("Enter a number of messages to generate (500 messages = 7.5 megabytes): ");
            int numberOfMessages = Convert.ToInt32(Console.ReadLine());

            //JSON generation
            JsonGenerator jsonGenerator = new JsonGenerator();
            string json = jsonGenerator.Generate(numberOfMessages);

            //Writing generated JSON in file
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
                        break;
                }
            }

            Console.Write("Would you like to run benchmark for JSON parser? Y/N: ");
            var selection = Console.ReadLine();
            switch (selection)
            {
                //Benchmark
                case "Y":
                    BenchmarkRunner.Run(typeof(Program).Assembly);
                    break;
                case "N":
                    JsonParser jsonParser = new JsonParser();
                    List<Message> messages = jsonParser.Parse(Files.Read(file));
                    MessageRepository.SaveMessages(messages);
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }    
}