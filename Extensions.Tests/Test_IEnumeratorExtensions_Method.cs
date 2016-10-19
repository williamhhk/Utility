using NUnit.Framework;
using ROOT.CIMV2.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ROOT.CIMV2.Win32.ComputerSystem;

namespace Extensions.Tests
{
    [TestFixture]
    public class Test_IEnumeratorExtensions_Method
    {
        private ComputerSystemCollection _system;
        [SetUp]
        public void Initialize()
        {
            _system = ComputerSystem.GetInstances();
        }
        [Test]
        public void Test_No_Error_IEnumeratorExtension_Method_1()
        {
            IEnumerable<string> list = new[] { "Arizona", "California" };
            var sequence = list.AsEnumerable<string>().Where(i => i.Contains("Arizona")).ToList();
            CollectionAssert.AreEqual(sequence, new[] { "Arizona"});
        }

        [Test]
        public void Test_No_Error_IEnumeratorExtension_Method_2()
        {
            var createClassName = _system.GetEnumerator().AsEnumerable<ComputerSystem>().Select(i => i.CreationClassName).ToList().FirstOrDefault();
            Assert.AreEqual(createClassName.ToString().ToUpper(), "WIN32_COMPUTERSYSTEM");
        }
    }
}
