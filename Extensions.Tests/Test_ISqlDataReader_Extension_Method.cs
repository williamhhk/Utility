using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Tests
{
    [TestFixture]
    class Test_ISqlDataReader_Extension_Method
    {
        //http://stackoverflow.com/questions/35200882/how-to-mock-idatareader-to-test-method-which-converts-sqldatareader-to-system-da

        private const string Column1 = "First";
        private const string Column2 = "Second";
        private const string Column3 = "Third";
        private const string Column4 = "Fourth";
        private const string ExpectedValue1 = "Value1";
        private const int ExpectedValue2 = 1;
        private const bool ExpectedValue3 = true;

        public Mock<IDataReader> CreateDataReader()
        {
            var dataReader = new Mock<IDataReader>();

            dataReader.Setup(m => m.FieldCount).Returns(3);
            dataReader.Setup(m => m.GetName(0)).Returns(Column1);
            dataReader.Setup(m => m.GetName(1)).Returns(Column2);
            dataReader.Setup(m => m.GetName(2)).Returns(Column3);

            dataReader.Setup(m => m.GetFieldType(0)).Returns(typeof(string));
            dataReader.Setup(m => m.GetFieldType(1)).Returns(typeof(int));
            dataReader.Setup(m => m.GetFieldType(2)).Returns(typeof(bool));

            dataReader.Setup(m => m.GetOrdinal("First")).Returns(0);
            dataReader.Setup(m => m.GetOrdinal("Second")).Returns(1);
            dataReader.Setup(m => m.GetOrdinal("Third")).Returns(2);

            dataReader.Setup(m => m.GetValue(0)).Returns(ExpectedValue1);
            dataReader.Setup(m => m.GetValue(1)).Returns(ExpectedValue2);
            dataReader.Setup(m => m.GetValue(2)).Returns(ExpectedValue3);

            dataReader.SetupSequence(m => m.Read())
                .Returns(true) 
                //.Returns(true)    comment out if need more than 1 row.
                .Returns(false);
            return dataReader;
        }

        [Test]
        public void Test_SqlDataReader_String()
        {
            var dataReader = CreateDataReader().Object;
            while (dataReader.Read())
            {
                var value = dataReader.Get<string>("First");
                Assert.AreEqual("Value1", value);
            }
        }

        [Test]
        public void Test_SqlDataReader_Int()
        {
            var dataReader = CreateDataReader().Object;
            while (dataReader.Read())
            {
                var value = dataReader.Get<int>("Second");
                Assert.AreEqual(1, value);
            }
        }

        [Test]
        public void Test_SqlDataReader_Bool()
        {
            var dataReader = CreateDataReader().Object;
            while (dataReader.Read())
            {
                var value = dataReader.Get<bool>("Third");
                Assert.AreEqual(true, value);
            }
        }
        //[Test]
        //public void ResolveDataReader_NamesColumn1()
        //{
        //    var dataReader = CreateDataReader().Object;
        //    while (dataReader.Read())
        //    {
        //        var value = dataReader.Get<string>("First");
        //        Assert.AreEqual(", value);
        //    }
        //    Assert.AreEqual(Column1, view.Table.Columns[0].ColumnName);
        //}

        //[Test]
        //public void ResolveDataReader_PopulatesColumn1()
        //{
        //    var dataReader = CreateDataReader().Object;
        //    while (dataReader.Read())
        //    {
        //        var value = dataReader.Get<string>("First");
        //        Assert.AreEqual("Value1", value);
        //    }
        //    Assert.AreEqual(ExpectedValue1, view.Table.Rows[0][0]);
        //}
    }
}
