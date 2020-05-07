using Newtonsoft.Json.Linq;
using System;

namespace JsonImporter.Json
{
    public class JsonValidator
    {
        public static bool IsJsonValid(string json)
        {
            try
            {
                var result = JArray.Parse(json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
