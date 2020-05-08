using JsonImporter.Json;
using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace JsonImporter.Tests.Json
{
    public class JsonParserTests
    {
        private static readonly string jsonValid =
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ExampleValid.json";

        private static readonly string jsonInvalid =
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ExampleInvalid.json";

        private static readonly string jsonInvalidInteger =
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ExampleInvalidInteger.json";

        private static readonly string jsonInvalidDate =
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ExampleInvalidDate.json";

        private static readonly string notJson = "some not a JSON content";

        [Fact]
        public void ReadJson_VALID()
        {
            JsonParser jsonReader = new JsonParser();
            List<Message> result = jsonReader.Parse(Files.Read(jsonValid));

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public void ReadJson_INVALID_JSON_CONTENT()
        {
            JsonParser jsonReader = new JsonParser();
            List<Message> result = jsonReader.Parse(Files.Read(jsonInvalid));

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ReadJson_INVALID_JSON_INTEGER()
        {
            JsonParser jsonReader = new JsonParser();
            List<Message> result = jsonReader.Parse(Files.Read(jsonInvalidInteger));

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ReadJson_INVALID_JSON_DATE()
        {
            JsonParser jsonReader = new JsonParser();
            List<Message> result = jsonReader.Parse(Files.Read(jsonInvalidDate));

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ReadJson_INVALID_JSON_NULL()
        {
            JsonParser jsonReader = new JsonParser();
            List<Message> result = jsonReader.Parse(null);

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