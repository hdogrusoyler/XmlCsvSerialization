using XmlCsvSerialization.Object;
using XmlCsvSerialization.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XmlCsvSerialization.Serialization.Concrete
{
    public class CsvSerialize : ISerializer
    {
        public void Serialize(string filename, AddressInfo aic)
        {
            List<String[]> list = new List<string[]>();

            foreach (AddressInfoCity AddInf in aic.City.ToList())
            {

                foreach (AddressInfoCityDistrict AddInfDist in AddInf.District)
                {

                    foreach (AddressInfoCityDistrictZip AddInfDistZip in AddInfDist.Zip)
                    {
                        List<String> l = new List<string>();
                        l.Add(AddInf.name);
                        l.Add(AddInf.code);
                        l.Add(AddInfDist.name);
                        l.Add(AddInfDistZip.code);
                        list.Add(l.ToArray());
                    }
                }


            }

            FileStream fs = File.Create(filename);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                foreach (String[] i in list)
                {
                    var a = String.Join(",", i);
                    writer.WriteLine(a);
                }
            }
        }

        public AddressInfo DeSerialize(string filename)
        {
            AddressInfo ai = new AddressInfo();

            FileStream fs = new FileStream(filename, FileMode.Open);
            using (StreamReader reader = new StreamReader(fs))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    String[] aic = line.Split(',');



                    //ai.City?.Where(i => i.name == aic.CityName && i.code == aic.CityCode) == null
                    if (ai.City == null || Array.Exists(ai.City, i => i.name == aic[0] && i.code == aic[1]) == false)
                    {
                        List<AddressInfoCity> lc = new List<AddressInfoCity>();
                        if (ai.City != null)
                        {
                            lc.AddRange(ai.City.ToList());
                        }
                        AddressInfoCity c = new AddressInfoCity();
                        c.name = aic[0];
                        c.code = aic[1];
                        c.District = new AddressInfoCityDistrict[] { };

                        List<AddressInfoCityDistrict> ld = new List<AddressInfoCityDistrict>();
                        if (c.District != null)
                        {
                            ld.AddRange(c.District.ToList());
                        }
                        AddressInfoCityDistrict d = new AddressInfoCityDistrict();
                        d.name = aic[2];
                        d.Zip = new AddressInfoCityDistrictZip[] { };

                        List<AddressInfoCityDistrictZip> lz = new List<AddressInfoCityDistrictZip>();
                        if (d.Zip != null)
                        {
                            lz.AddRange(d.Zip.ToList());
                        }
                        AddressInfoCityDistrictZip z = new AddressInfoCityDistrictZip();
                        z.code = aic[3];

                        lz.Add(z);
                        d.Zip = lz.ToArray();

                        ld.Add(d);
                        c.District = ld.ToArray();

                        lc.Add(c);
                        ai.City = lc.ToArray();

                    }
                    else
                    {
                        foreach (AddressInfoCity aicv in ai.City)
                        {
                            if (aicv.name == aic[0] && aicv.code == aic[1])
                            {
                                if (aicv.District == null || Array.Exists(aicv.District, l => l.name == aic[2]) == false)
                                {
                                    List<AddressInfoCityDistrict> ld = new List<AddressInfoCityDistrict>();
                                    if (aicv.District != null)
                                    {
                                        ld.AddRange(aicv.District.ToList());
                                    }
                                    AddressInfoCityDistrict d = new AddressInfoCityDistrict();
                                    d.name = aic[2];
                                    d.Zip = new AddressInfoCityDistrictZip[] { };

                                    List<AddressInfoCityDistrictZip> lz = new List<AddressInfoCityDistrictZip>();
                                    if (d.Zip != null)
                                    {
                                        lz.AddRange(d.Zip.ToList());
                                    }
                                    AddressInfoCityDistrictZip z = new AddressInfoCityDistrictZip();
                                    z.code = aic[3];

                                    lz.Add(z);
                                    d.Zip = lz.ToArray();

                                    ld.Add(d);
                                    aicv.District = ld.ToArray();
                                }
                                else
                                {
                                    foreach (AddressInfoCityDistrict aicdv in aicv.District)
                                    {
                                        if (aicdv.name == aic[2])
                                        {
                                            if (aicdv.Zip == null || Array.Exists(aicdv.Zip, m => m.code == aic[3]) == false)
                                            {
                                                List<AddressInfoCityDistrictZip> lz = new List<AddressInfoCityDistrictZip>();
                                                if (aicdv.Zip != null)
                                                {
                                                    lz.AddRange(aicdv.Zip.ToList());
                                                }
                                                AddressInfoCityDistrictZip z = new AddressInfoCityDistrictZip { };
                                                z.code = aic[3];
                                                lz.Add(z);

                                                aicdv.Zip = lz.ToArray();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ai;
        }
    }
}
