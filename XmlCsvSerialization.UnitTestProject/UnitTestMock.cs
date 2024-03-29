﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using XmlCsvSerialization.Object;
using XmlCsvSerialization.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestMock
    {
        [TestMethod]
        public void TestMethod1()
        {
            var _xmlSerialize = new Mock<ISerializer>();
            _xmlSerialize.Setup(f => f.Serialize(It.Ref<AddressInfo>.IsAny, It.IsAny<string>()));
            var _csvSerialize = new Mock<ISerializer>();
            _csvSerialize.Setup(f => f.DeSerialize(It.IsAny<string>())).Returns(It.Ref<AddressInfo>.IsAny);

            try
            {
                var a = _csvSerialize.Object.DeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.csv");

                a.City = a.City.Where(c => c.name == "Antalya").ToArray();

                _xmlSerialize.Object.Serialize(a, "C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\CsvToXml.xml");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, " ");
            }

        }

        [TestMethod]
        public void TestMethod2()
        {
            var _csvSerialize = new Mock<ISerializer>();
            _csvSerialize.Setup(f => f.DeSerialize(It.IsAny<string>())).Returns(It.Ref<AddressInfo>.IsAny);
            _csvSerialize.Setup(f => f.Serialize(It.Ref<AddressInfo>.IsAny, It.IsAny<string>()));

            try
            {
                var a = _csvSerialize.Object.DeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.csv");

                a.City = a.City.OrderBy(d => d.name).ToArray();
                foreach (AddressInfoCity aic in a.City)
                {
                    aic.District.OrderBy(d => d.name);
                }

                _csvSerialize.Object.Serialize(a, "C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\CsvToCsv.csv");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, " ");
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            var _xmlSerialize = new Mock<ISerializer>();
            _xmlSerialize.Setup(f => f.DeSerialize(It.IsAny<string>())).Returns(It.Ref<AddressInfo>.IsAny);
            var _csvSerialize = new Mock<ISerializer>();
            _csvSerialize.Setup(f => f.Serialize(It.Ref<AddressInfo>.IsAny, It.IsAny<string>()));

            try
            {
                var a = _xmlSerialize.Object.DeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.xml");

                a.City = a.City.Where(c => c.name == "Ankara").ToArray();
                foreach (AddressInfoCity aic in a.City)
                {
                    foreach (AddressInfoCityDistrict aicd in aic.District)
                    {
                        aicd.Zip = aicd.Zip.OrderByDescending(z => z.code).ToArray();
                    }
                }

                _csvSerialize.Object.Serialize(a, "C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\XmlToCsv.csv");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, " ");
            }
        }
    }
}
