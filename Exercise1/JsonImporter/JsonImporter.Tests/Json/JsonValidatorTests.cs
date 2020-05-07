using Xunit;
using JsonImporter.Json;

namespace JsonImporter.Tests.Json
{
    public class JsonValidatorTests
    {
        private static readonly string jsonValid = @"
            [
	            {
		            'name': 'James',
		            'hobbies': ['.NET', 'Blogging', 'Reading', 'Xbox', 'LOLCATS']
	            },
	            {
		            'name': 'John',
		            'hobbies': ['Java', 'Blogging', 'Reading', 'Xbox', 'LOLCATS']
	            }
            ]";
        private static readonly string jsonWithErrors = @"
            [
	            
		            'name': 'James',
		            'hobbies': ['.NET', 'Blogging', 'Reading', 'Xbox', 'LOLCATS']
	            },
	            {
		            'name': 'John',
		            'hobbies': ['Java', 'Blogging', 'Reading', 'Xbox', 'LOLCATS']
	            }
            ]";
        private static readonly string notJson = "some invalid json content";
        

        [Fact]
        public void JsonValidator_VALID()
        {
            Assert.True(JsonValidator.IsJsonValid(jsonValid));
        }

        [Fact]
        public void JsonValidator_INVALID()
        {
            Assert.False(JsonValidator.IsJsonValid(notJson));
        }

        [Fact]
        public void JsonValidator_INVALID_WITH_ERRORS()
        {
            Assert.False(JsonValidator.IsJsonValid(jsonWithErrors));
        }
    }
}
