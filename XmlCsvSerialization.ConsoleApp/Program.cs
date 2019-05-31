using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using XmlCsvSerialization.Object;
using Serialize;
using XmlCsvSerialization.Serialization;
using Serialize.Concrete.Csv;
using Serialize.Concrete.Xml;

namespace ConsoleApp
{
    public class Program
    {
        private ISerializer serviceCsv;
        private ISerializer serviceXml;
        public Program()
        {
            serviceCsv = new CsvSerial();
            serviceXml = new XmlSerial();
        }
        static void Main(string[] args)
        {
            Program p = new Program();

            p.TestCase1();
            p.TestCase2();
            p.TestCase3();
        }

        public void TestCase1()
        {
            var a = serviceCsv.DeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.csv");

            a.City = a.City.Where(c => c.name == "Antalya").ToArray();

            serviceXml.Serialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\CsvToXml.xml", a);

            Console.WriteLine("CsvToXml Done!");
        }

        public void TestCase2()
        {
            var a = serviceCsv.DeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.csv");

            a.City = a.City.OrderBy(c => c.name).ToArray();
            foreach (AddressInfoCity aic in a.City)
            {
                aic.District.OrderBy(d => d.name);
            }

            serviceCsv.Serialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\CsvToCsv.csv", a);

            Console.WriteLine("CsvToCsv Done!");
        }

        public void TestCase3()
        {
            var a = serviceXml.DeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.xml");

            a.City = a.City.Where(c => c.name == "Ankara").ToArray();
            foreach (AddressInfoCity aic in a.City)
            {
                foreach (AddressInfoCityDistrict aicd in aic.District)
                {
                    aicd.Zip = aicd.Zip.OrderByDescending(z => z.code).ToArray();
                }
            }

            serviceCsv.Serialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\XmlToCsv.csv", a);

            Console.WriteLine("XmlToCsv Done!");
        }
    }
}
