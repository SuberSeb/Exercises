using System;
using System.IO;

namespace JsonImporter.Tools
{
    public class Files
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void GetInfoInLog(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            logger.Info("Full file path: {path}. File size is {size} bytes.", fileInfo.FullName, fileInfo.Length);
        }

        public static string Read(string path)
        {
            string fileContent = String.Empty;

            if (File.Exists(path))
            {
                try
                {
                    using StreamReader reader = new StreamReader(path);
                    fileContent = reader.ReadToEnd();
                    logger.Info("Reading from file successful");
                }
                catch (Exception ex)
                {
                    logger.Error("Error while reading from file: " + ex);
                }
            }
            else
            {
                logger.Error("File does not exist");
            }

            return fileContent;
        }

        public static void Write(string path, string content)
        {
            try
            {
                if (content != String.Empty)
                {
                    using var writer = new StreamWriter(path, false);
                    writer.WriteLine(content.ToString());
                    writer.Close();

                    Files.GetInfoInLog(path);
                    logger.Info("Write to file {path} was successful", path);
                }
                else
                {
                    logger.Error("Error - writing empty string to file");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error while writing to file {path}: {ex}", path, ex);
            }
        }
    }
}