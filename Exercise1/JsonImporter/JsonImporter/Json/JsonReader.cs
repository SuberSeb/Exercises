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
            string jsonResult = String.Empty;

            if(File.Exists(path))
            {
                try
                {
                    using StreamReader reader = new StreamReader(path);
                    jsonResult = await reader.ReadToEndAsync();
                    logger.Info("Reading from file successful");

                    if(JsonValidator.IsJsonValid(jsonResult))
                    {
                        try
                        {
                            var json = JsonConvert.DeserializeObject<List<Message>>(jsonResult);
                            logger.Info("JSON deserialization successful");
                        }
                        catch (JsonException ex)
                        {
                            logger.Error("Error while deserialization JSON: " + ex);
                        }
                    }                  
                    else
                    {
                        logger.Error("JSON file is not valid");
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Error while reading from file: " + ex);
                }                               
            }
            else
            {
                logger.Error("File does not exist");
            }            
        }
    }
}