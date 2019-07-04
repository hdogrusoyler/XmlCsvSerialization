using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using XmlCsvSerialization.Extension;
using XmlCsvSerialization.Object;
using XmlCsvSerialization.Serialization;
using XmlCsvSerialization.Serialization.Concrete;

namespace ConsoleApp
{
    public class Program
    {
        private ISerializer serviceCsv;
        private ISerializer serviceXml;
        private ISerializer serviceJson;

        public Program()
        {
            serviceCsv = new CsvSerialize();
            serviceXml = new XmlSerialize();
            serviceJson = new JsonSerialize();
        }
        static void Main(string[] args)
        {
            Program p = new Program();

            p.TestCaseExtension1();
            p.TestCaseExtension2();

            p.TestCase1();
            p.TestCase2();
            p.TestCase3();
        }

        public void TestCaseExtension1()
        {
            System.Console.WriteLine("CsvDeSerialize");
            var a = serviceCsv.DeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.csv");
            System.Console.WriteLine("JsonSerialization Extension");
            String aiSt = a.WriteJsonFromObject(serviceJson);
            System.Console.WriteLine(aiSt);
            System.Console.WriteLine("JsonDeSerialization Extension");
            AddressInfo ai = aiSt.ReadJsonToObject(serviceJson);
            foreach (AddressInfoCity city in ai.City)
                Console.WriteLine(city.name);
        }

        public void TestCaseExtension2()
        {
            System.Console.WriteLine("XmlDeSerialize");
            var a = serviceXml.DeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.xml");
            System.Console.WriteLine("JsonSerialization Extension");
            String aiSt = a.WriteJsonFromObject(serviceJson);
            System.Console.WriteLine(aiSt);
            System.Console.WriteLine("JsonDeSerialization Extension");
            AddressInfo ai = aiSt.ReadJsonToObject(serviceJson);
            foreach (AddressInfoCity city in ai.City)
                Console.WriteLine(city.name);
        }

        public void TestCase1()
        {
            var a = serviceCsv.DeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.csv");

            a.City = a.City.Where(c => c.name == "Antalya").ToArray();

            serviceXml.Serialize(a, "C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\CsvToXml.xml");

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

            serviceCsv.Serialize(a, "C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\CsvToCsv.csv");

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

            serviceCsv.Serialize(a, "C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\XmlToCsv.csv");

            Console.WriteLine("XmlToCsv Done!");
        }
    }
}
