using XmlCsvSerialization.Object;
using XmlCsvSerialization.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlCsvSerialization.Serialization.Concrete
{
    public class XmlSerial : ISerializer
    {
        public void Serialize(string filename, AddressInfo ai)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AddressInfo));
            StreamWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, ai);
            writer.Close();
        }

        public AddressInfo DeSerialize(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AddressInfo));
            FileStream fs = new FileStream(filename, FileMode.Open);
            //StreamReader reader = new StreamReader(filename);
            AddressInfo ai;
            ai = (AddressInfo)serializer.Deserialize(fs);
            //fs.Close();
            return ai;
        }
    }
}
