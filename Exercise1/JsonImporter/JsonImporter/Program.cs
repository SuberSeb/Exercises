using System;
using JsonImporter.Tools;

namespace JsonImporter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            JsonGenerator jsonGenerator = new JsonGenerator();
            jsonGenerator.GenerateJson(@"C:\Users\SuberSeb\Desktop\message.json");

            Console.WriteLine();
        }
    }
}