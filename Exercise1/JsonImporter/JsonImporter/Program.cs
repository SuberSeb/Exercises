using JsonImporter.Json;
using System;
using System.Threading.Tasks;

namespace JsonImporter
{
    internal class Program
    {
        private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private static async Task Main(string[] args)
        {
            string path = desktopPath + @"\message.json";
            logger.Info("File 'message.json' will be saved in " + desktopPath);

            JsonGenerator jsonGenerator = new JsonGenerator();
            jsonGenerator.GenerateJson(path);

            JsonReader jsonReader = new JsonReader();
            await jsonReader.ReadJsonAsync(path);

            Console.WriteLine("Press any button to exit...");
            Console.ReadKey();
        }
    }
}