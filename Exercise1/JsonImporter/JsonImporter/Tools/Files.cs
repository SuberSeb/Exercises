using System;
using System.IO;

namespace JsonImporter.Tools
{
    public class Files
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

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
                if (content != String.Empty && Directory.Exists(path.Replace(Path.GetFileName(path), String.Empty)))
                {
                    using var writer = new StreamWriter(path, false);
                    writer.WriteLine(content.ToString());
                    writer.Close();

                    FileInfo fileInfo = new FileInfo(path);
                    logger.Info("Write to file {path} was successful. Filesize is {size} bytes", path, fileInfo.Length);
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