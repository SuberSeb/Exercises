using JsonImporter.Json;
using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace JsonImporter.Tests.Json
{
    public class JsonReaderTests
    {
        private static readonly string jsonValidExampleFile =
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ExampleValid.json";

        private static readonly string jsonInvalidContentExampleFile =
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ExampleInvalid.json";

        private static readonly string notJson = "some not a JSON content";

        [Fact]
        public void ReadJson_VALID()
        {
            JsonParser jsonReader = new JsonParser();
            List<Message> result = jsonReader.Parse(Files.Read(jsonValidExampleFile));

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public void ReadJson_INVALID_JSON_CONTENT()
        {
            JsonParser jsonReader = new JsonParser();
            List<Message> result = jsonReader.Parse(Files.Read(jsonInvalidContentExampleFile));

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ReadJson_INVALID_FILE()
        {
            JsonParser jsonReader = new JsonParser();
            List<Message> result = jsonReader.Parse(notJson);

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}