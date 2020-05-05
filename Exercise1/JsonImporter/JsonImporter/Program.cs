using JsonImporter.Tools;
using System;
using System.Threading.Tasks;

namespace JsonImporter
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Enter file path for 'message.json'.");
            string path = Console.ReadLine();

            JsonReader jsonReader = new JsonReader();
            await jsonReader.ReadJsonAsync(path);

            Console.WriteLine();
            Console.WriteLine("Press any button to exit.");
            Console.ReadKey();
        }
    }
}