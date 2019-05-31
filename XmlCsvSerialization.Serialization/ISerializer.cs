using XmlCsvSerialization.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlCsvSerialization.Serialization
{
    public interface ISerializer
    {
        void Serialize(string filename, AddressInfo aic);
        AddressInfo DeSerialize(string filename);
    }
}
