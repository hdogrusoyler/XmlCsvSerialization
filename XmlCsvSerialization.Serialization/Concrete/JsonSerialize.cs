using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using XmlCsvSerialization.Object;

namespace XmlCsvSerialization.Serialization.Concrete
{
    public class JsonSerialize : ISerializer
    {
        public AddressInfo DeSerialize(string json)
        {
            AddressInfo ai = new AddressInfo();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(ai.GetType());
            ai = ser.ReadObject(ms) as AddressInfo;
            ms.Close();
            return ai;
        }

        public string Serialize(AddressInfo ai, string jsons = null)
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AddressInfo));
            ser.WriteObject(ms, ai);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }
    }
}
