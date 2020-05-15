using JsonImporter.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JsonImporter.Tools
{
    public class Serializer
    {
        public static byte[] SerializeMessage(object message)
        {
            if (message == null)
                return null;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, message);
                return memoryStream.ToArray();
            }
        }

        public static Message DeserializeMessage(byte[] serializedMessage)
        {
            if (serializedMessage == null)
                return null;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(serializedMessage))
            {
                return (Message)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}
