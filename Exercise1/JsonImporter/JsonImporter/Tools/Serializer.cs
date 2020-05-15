using JsonImporter.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JsonImporter.Tools
{
    public class Serializer
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static byte[] SerializeMessage(object message)
        {
            if (message == null)
            {
                logger.Error("Can't serialize object: null");
                return null;
            }                

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, message);
                    logger.Info("Object was serialized successfully.");
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error while serializing object: " + ex);
                return null;
            }            
        }

        public static Message DeserializeMessage(byte[] serializedMessage)
        {
            if (serializedMessage == null)
            {
                logger.Error("Can't deserialize bytes array: null");
                return null;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(serializedMessage))
                {
                    return (Message)binaryFormatter.Deserialize(memoryStream);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error while serializing object: " + ex);
                return null;
            }
        }
    }
}
