using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using JsonImporter.Database;
using JsonImporter.Json;
using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;

namespace JsonImporter
{
    [SimpleJob(launchCount: 5)]
    public class ParserBenchmark
    {
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\message.json";
        private readonly JsonParser jsonParser;
        private readonly string jsonContent;

        public ParserBenchmark()
        {
            jsonParser = new JsonParser();
            jsonContent = Files.Read(path);
        }

        [Benchmark]
        public List<Message> Parser() => jsonParser.Parse(jsonContent);
    }

    public class Program
    {
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\message.json";        

        public static void Main(string[] args)
        {
            Console.Write("Enter a number of messages to generate (500 messages = 7.5 megabytes): ");
            int numberOfMessages = Convert.ToInt32(Console.ReadLine());

            //JSON generation
            JsonGenerator jsonGenerator = new JsonGenerator();
            string json = jsonGenerator.Generate(numberOfMessages);

            //Writing generated JSON in file
            Files.Write(path, json);

            Console.Write("Would you like to run benchmark for JSON parser? Y/N: ");
            var selection = Console.ReadLine();
            switch (selection)
            {
                //Benchmark
                case "Y":
                    BenchmarkRunner.Run(typeof(Program).Assembly);
                    break;
                case "N":
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