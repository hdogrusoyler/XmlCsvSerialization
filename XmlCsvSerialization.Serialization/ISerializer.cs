using XmlCsvSerialization.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlCsvSerialization.Serialization
{
    public interface ISerializer
    {
        String Serialize(AddressInfo aic, string name);
        AddressInfo DeSerialize(string name);
    }
}
