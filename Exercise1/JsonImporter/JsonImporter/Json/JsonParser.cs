using JsonImporter.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonImporter.Json
{
    public class JsonParser
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public List<Message> Parse(string fileContent)
        {
            List<Message> messages = new List<Message>();

            if (JsonValidator.IsJsonValid(fileContent))
            {
                try
                {
                    messages = JsonConvert.DeserializeObject<List<Message>>(fileContent);
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

            return messages;
        }
    }
}