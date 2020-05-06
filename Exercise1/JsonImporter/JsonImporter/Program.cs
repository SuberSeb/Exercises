using JsonImporter.Json;
using System;
using System.Threading.Tasks;

namespace JsonImporter
{
    internal class Program
    {
        private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static readonly string fileName = "message.json";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Enter number of messages: ");
            int numberOfMessages = Convert.ToInt32(Console.ReadLine());

            JsonGenerator jsonGenerator = new JsonGenerator();
            jsonGenerator.GenerateJson(desktopPath, fileName, numberOfMessages);

            JsonReader jsonReader = new JsonReader();
            await jsonReader.ReadJsonAsync(desktopPath + @"\" + fileName);

            Console.WriteLine("Press any button to exit...");
            Console.ReadKey();
        }
    }
}