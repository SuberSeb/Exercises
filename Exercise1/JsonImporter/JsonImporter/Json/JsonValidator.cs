using Newtonsoft.Json.Linq;
using System;

namespace JsonImporter.Json
{
    public class JsonValidator
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static bool IsJsonValid(string json)
        {
            try
            {
                var result = JArray.Parse(json);
                logger.Info("JSON is valid");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("JSON is not valid: " + ex);
                return false;
            }
        }
    }
}