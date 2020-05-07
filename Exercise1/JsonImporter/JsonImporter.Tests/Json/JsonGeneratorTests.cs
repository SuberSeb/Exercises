using System;
using System.IO;
using JsonImporter.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace JsonImporter.Tests.Json
{
    public class JsonGeneratorTests
    {
        private static readonly string pathValid = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);        
        private static readonly string pathInvalid = "some invalid path";
        private static readonly string fileName = "message.json";

        private void DeleteFileIfExists()
        {
            try
            { 
                if (File.Exists(Path.Combine(pathValid, fileName)))
                    File.Delete(Path.Combine(pathValid, fileName));
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public void GenerateJson_VALID_ALL()
        {
            DeleteFileIfExists();

            JsonGenerator jsonGenerator = new JsonGenerator();
            string result = jsonGenerator.GenerateJson(pathValid, fileName, 10);

            Assert.NotEmpty(result);
            Assert.True(JsonValidator.IsJsonValid(result));
            Assert.True(File.Exists(pathValid + @"\" + fileName));

            DeleteFileIfExists();
        }

        [Fact]
        public void GenerateJson_INVALID_NUMBER()
        {
            DeleteFileIfExists();

            JsonGenerator jsonGenerator = new JsonGenerator();
            string result = jsonGenerator.GenerateJson(pathValid, fileName, -1);

            Assert.Empty(result);
            Assert.False(JsonValidator.IsJsonValid(result));
            Assert.False(File.Exists(pathValid + @"\" + fileName));

            DeleteFileIfExists();
        }

        [Fact]
        public void GenerateJson_INVALID_PATH()
        {
            DeleteFileIfExists();

            JsonGenerator jsonGenerator = new JsonGenerator();
            string result = jsonGenerator.GenerateJson(pathInvalid, fileName, 10);

            Assert.Empty(result);
            Assert.False(JsonValidator.IsJsonValid(result));
            Assert.False(File.Exists(pathInvalid + @"\" + fileName));

            DeleteFileIfExists();
        }

        [Fact]
        public void GenerateJson_INVALID_FILENAME()
        {
            DeleteFileIfExists();

            JsonGenerator jsonGenerator = new JsonGenerator();
            string result = jsonGenerator.GenerateJson(pathValid, String.Empty, 10);

            Assert.Empty(result);
            Assert.False(JsonValidator.IsJsonValid(result));
            Assert.False(File.Exists(pathValid + @"\" + String.Empty));

            DeleteFileIfExists();
        }
    }
}
