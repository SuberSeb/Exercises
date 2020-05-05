using System;
using System.Threading.Tasks;
using JsonImporter.Tools;

namespace JsonImporter
{
    internal class Program
    {
        private static readonly string path = @"C:\Users\SuberSeb\Desktop\message.json";

        private static async Task Main(string[] args)
        {
            JsonReader jsonReader = new JsonReader();
            await jsonReader.ReadJsonAsync(path);           

            Console.ReadLine();
        }
    }
}