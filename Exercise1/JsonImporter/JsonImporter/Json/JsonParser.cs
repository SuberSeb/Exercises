﻿using JsonImporter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
                    var timer = new Stopwatch();
                    timer.Start();
                    messages = JsonConvert.DeserializeObject<List<Message>>(fileContent);
                    timer.Stop();
                    Console.WriteLine($"JSON deserialization successful. Elapsed time: {timer.ElapsedMilliseconds} ms");
                    logger.Info($"JSON deserialization successful. Elapsed time: {timer.ElapsedMilliseconds} ms");
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