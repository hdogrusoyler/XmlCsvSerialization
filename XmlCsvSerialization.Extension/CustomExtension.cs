using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using XmlCsvSerialization.Object;
using XmlCsvSerialization.Serialization;

namespace XmlCsvSerialization.Extension
{
    public static class CustomExtension
    {
        // Create a AddressInfo object and serialize it to a JSON stream.
        public static string WriteJsonFromObject(this AddressInfo ai, ISerializer jsonserializer)
        {
            return jsonserializer.Serialize(ai, null);
        }

        // Deserialize a JSON stream to a AddressInfo object.  
        public static AddressInfo ReadJsonToObject(this string json, ISerializer jsonserializer)
        {
            return jsonserializer.DeSerialize(json);
        }
    }
}
