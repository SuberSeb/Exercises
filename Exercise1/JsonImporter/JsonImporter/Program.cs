using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using JsonImporter.Json;
using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;

namespace JsonImporter
{
    public class Program
    {
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\message.json";

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

        public static void Main(string[] args)
        {
            int numberOfMessages = 500;

            //JSON generation
            JsonGenerator jsonGenerator = new JsonGenerator();
            string json = jsonGenerator.Generate(numberOfMessages);

            //Writing generated JSON in file
            Files.Write(path, json);

            //Benchmark
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }    
}