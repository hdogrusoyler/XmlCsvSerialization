using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using XmlCsvSerialization.Object;

namespace XmlCsvSerialization.Extension
{
    public static class CustomExtension
    {
        // Create a AddressInfo object and serialize it to a JSON stream.
        public static string WriteJsonFromObject(this AddressInfo ai)
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AddressInfo));
            ser.WriteObject(ms, ai);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        // Deserialize a JSON stream to a AddressInfo object.  
        public static AddressInfo ReadJsonToObject(this string json)
        {
            AddressInfo ai = new AddressInfo();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(ai.GetType());
            ai = ser.ReadObject(ms) as AddressInfo;
            ms.Close();
            return ai;
        }
    }
}
