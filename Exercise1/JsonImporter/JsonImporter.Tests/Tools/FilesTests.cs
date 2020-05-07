using JsonImporter.Tools;
using System;
using System.IO;
using Xunit;

namespace JsonImporter.Tests.Tools
{
    public class FilesTests
    {
        private static readonly string validFile =
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ExampleValid.json";

        private static readonly string pathForWrite =
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UnitTestFile.txt";

        private static readonly string invalidPath = "some invalid path";
        private static readonly string validContent = "some content";

        [Fact]
        public void Read_VALID_ALL()
        {
            string result = Files.Read(validFile);

            Assert.True(result != String.Empty);
            Assert.True(result == File.ReadAllText(validFile));
        }

        [Fact]
        public void Read_INVALID_PATH()
        {
            string result = Files.Read(invalidPath);
            Assert.True(result == String.Empty);
        }

        [Fact]
        public void Write_VALID_ALL()
        {
            if (File.Exists(pathForWrite))
                File.Delete(pathForWrite);

            Files.Write(pathForWrite, validContent);

            Assert.True(File.Exists(pathForWrite));
            Assert.True(File.ReadAllText(pathForWrite).Replace("\r\n", string.Empty) == validContent);
        }

        [Fact]
        public void Write_INVALID_PATH()
        {
            if (File.Exists(invalidPath))
                File.Delete(invalidPath);

            Files.Write(invalidPath, validContent);

            Assert.False(File.Exists(invalidPath));
        }

        [Fact]
        public void Write_INVALID_CONTENT_EMPTY()
        {
            if (File.Exists(pathForWrite))
                File.Delete(pathForWrite);

            Files.Write(pathForWrite, String.Empty);

            Assert.False(File.Exists(pathForWrite));
        }
    }
}