using JsonImporter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JsonImporter.Tools
{
    class JsonReader
    {
        public async Task ReadJsonAsync(string path)
        {
            string messages = "";

            JsonGenerator jsonGenerator = new JsonGenerator();
            jsonGenerator.GenerateJson(path);

            try
            {
                using StreamReader reader = new StreamReader(path);
                messages = await reader.ReadToEndAsync();
                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var json = JsonConvert.DeserializeObject<List<Message>>(messages);
        }
    }
}
