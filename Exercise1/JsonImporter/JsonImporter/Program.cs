using JsonImporter.Json;
using System;
using System.Threading.Tasks;

namespace JsonImporter
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //Console.WriteLine("Enter full file path for 'message.json'.");
            //string path = Console.ReadLine();

            string path = @"C:\Users\SuberSeb\Desktop\message.json";

            JsonReader jsonReader = new JsonReader();
            await jsonReader.ReadJsonAsync(path);

            Console.WriteLine();
            Console.WriteLine("Press any button to exit.");
            Console.ReadKey();
        }
    }
}