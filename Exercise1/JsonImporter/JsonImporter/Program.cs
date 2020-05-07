using JsonImporter.Json;
using JsonImporter.Tools;
using JsonImporter.Models;
using System;
using System.Collections.Generic;

namespace JsonImporter
{
    internal class Program
    {
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\message.json";

        private static void Main(string[] args)
        {
            //Количество сообщений в JSON
            Console.WriteLine("Enter number of messages: ");
            int numberOfMessages = Convert.ToInt32(Console.ReadLine());

            //Генерация JSON 
            JsonGenerator jsonGenerator = new JsonGenerator();
            string json = jsonGenerator.GenerateJson(numberOfMessages);

            Console.WriteLine("JSON generation complete.");

            //Запись JSON
            Files.Write(path, json);

            Console.WriteLine("JSON write to file complete.");

            //Чтение из JSON 
            JsonReader jsonReader = new JsonReader();
            List<Message> messages = jsonReader.ReadJson(path);

            Console.WriteLine("JSON reading complete.");

            Console.WriteLine("Press any button to exit...");
            Console.ReadKey();
        }
    }
}