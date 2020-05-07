using JsonImporter.Json;
using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;

namespace JsonImporter
{
    internal class Program
    {
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\message.json";

        private static void Main(string[] args)
        {
            //Enter number of messages
            Console.WriteLine("Enter number of messages: ");
            Console.WriteLine("(About 500 messages will occupy 7.5 Mbytes on disk)");
            int numberOfMessages = Convert.ToInt32(Console.ReadLine());

            //JSON generation
            JsonGenerator jsonGenerator = new JsonGenerator();
            string json = jsonGenerator.Generate(numberOfMessages);
            Console.WriteLine("JSON generation complete.");

            //Writing generated JSON in fuile
            Files.Write(path, json);
            Console.WriteLine("JSON write to file complete.");

            //Read generated JSON from file
            JsonParser jsonParser = new JsonParser();
            List<Message> messages = jsonParser.Parse(Files.Read(path));
            Console.WriteLine("JSON reading complete.");

            Console.WriteLine("Press any button to exit...");
            Console.ReadKey();
        }
    }
}