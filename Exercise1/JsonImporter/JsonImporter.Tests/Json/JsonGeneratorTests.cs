using JsonImporter.Json;
using System;
using Xunit;

namespace JsonImporter.Tests.Json
{
    public class JsonGeneratorTests
    {
        [Fact]
        public void GenerateJson_VALID()
        {
            JsonGenerator jsonGenerator = new JsonGenerator();
            string result = jsonGenerator.GenerateJson(10);

            Assert.True(result != String.Empty);
            Assert.True(JsonValidator.IsJsonValid(result));
        }

        [Fact]
        public void GenerateJson_INVALID_NUMBER()
        {

            JsonGenerator jsonGenerator = new JsonGenerator();
            string result = jsonGenerator.GenerateJson(-1);

            Assert.True(result == String.Empty);
            Assert.False(JsonValidator.IsJsonValid(result));
        }
    }
}