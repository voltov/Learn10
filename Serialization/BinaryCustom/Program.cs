using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinaryCustom
{
    [Serializable]
    public class SerializableClass : ISerializable
    {
        public string Property1 { get; set; }
        public int Property2 { get; set; }

        public SerializableClass() { }

        protected SerializableClass(SerializationInfo info, StreamingContext context)
        {
            Property1 = info.GetString("Property1");
            Property2 = info.GetInt32("Property2");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Property1", Property1);
            info.AddValue("Property2", Property2);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a SimpleClass object
            var simpleObject = new SerializableClass
            {
                Property1 = "Hello",
                Property2 = 123
            };

            // Serialize to binary format
            var binaryFormatter = new BinaryFormatter();
            using (var binaryStream = new FileStream("vovaObject.dat", FileMode.Create))
            {
                binaryFormatter.Serialize(binaryStream, simpleObject);
            }

            // Deserialize from binary format
            SerializableClass deserializedSimpleObject;
            using (var binaryStream = new FileStream("vovaObject.dat", FileMode.Open))
            {
                deserializedSimpleObject = (SerializableClass)binaryFormatter.Deserialize(binaryStream);
            }

            // Output the deserialized object to verify
            Console.WriteLine("Deserialized from binary:");
            Console.WriteLine($"Property1: {deserializedSimpleObject.Property1}");
            Console.WriteLine($"Property2: {deserializedSimpleObject.Property2}");
        }
    }
}
