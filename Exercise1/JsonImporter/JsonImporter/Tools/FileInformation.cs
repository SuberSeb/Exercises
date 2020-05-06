using System.IO;

namespace JsonImporter.Tools
{
    internal class FileInformation
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void GetFileInfoInLog(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            logger.Info("Full file path: {path}. File size is {size} bytes.", fileInfo.FullName, fileInfo.Length);
        }
    }
}