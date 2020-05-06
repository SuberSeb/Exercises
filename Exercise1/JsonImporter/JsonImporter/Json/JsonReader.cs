using JsonImporter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JsonImporter.Json
{
    public class JsonReader
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public async Task ReadJsonAsync(string path)
        {
            string messages = "";

            try
            {
                using StreamReader reader = new StreamReader(path);
                messages = await reader.ReadToEndAsync();
                logger.Info("Reading from file successful");
            }
            catch (Exception ex)
            {
                logger.Error("Error while reading from file: " + ex);
            }

            try
            {
                var json = JsonConvert.DeserializeObject<List<Message>>(messages);
                logger.Info("JSON deserialization successful");
            }
            catch (JsonException ex)
            {
                logger.Error("Error while deserialization JSON: " + ex);
            }
        }
    }
}