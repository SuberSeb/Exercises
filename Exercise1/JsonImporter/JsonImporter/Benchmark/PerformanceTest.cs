using BenchmarkDotNet.Attributes;
using JsonImporter.Json;
using JsonImporter.Models;
using JsonImporter.Queue;
using JsonImporter.Repositories;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;

namespace JsonImporter.Benchmark
{
    public class PerformanceTest
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
            private List<Message> messages = new List<Message>();

            public DbImporterBenchmark()
            {
                jsonParser = new JsonParser();
                messages = jsonParser.Parse(Files.Read(file));
            }

            [Benchmark]
            public void Importer() => MessageRepository.SaveMessagesBulk(messages);
        }

        [SimpleJob(launchCount: 3)]
        public class KafkaBenchmark
        {
            private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            private static readonly string file = desktopPath + @"\message.json";
            private readonly JsonParser jsonParser;
            private List<Message> messages = new List<Message>();

            public KafkaBenchmark()
            {
                jsonParser = new JsonParser();
                messages = jsonParser.Parse(Files.Read(file));
            }

            [Benchmark]
            public void SendToKafka() => Kafka.SendMessages(messages);
        }
    }
}