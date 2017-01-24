using NUnit.Framework;
using ROOT.CIMV2.Win32;
using static ROOT.CIMV2.Win32.ComputerSystem;
using System.Linq;
using Functional.Monads;
using System.Collections.Generic;
using Extension.Methods;

namespace ROOT.CIMV2.Win32.Tests
{
    [TestFixture]
    public class Class1
    {

        private ComputerSystemCollection _system;
        [SetUp]
        public void Initialize()
        {
            _system = ComputerSystem.GetInstances();
        }
        [Test]
        public void Test_Win32_ComputerSystem_No_Error_Win32_ComputerSystem()
        {
            var createClassName = _system.GetEnumerator().AsEnumerable<ComputerSystem>().Select(i => i.CreationClassName).ToList().FirstOrDefault();
            Assert.AreEqual(createClassName.ToString().ToUpper(), "WIN32_COMPUTERSYSTEM");
        }


        [Test]
        public void Test_Win32_ComputerSystem_No_Error_Manufacturer()
        {
            var manufacturer = _system.GetEnumerator().AsEnumerable<ComputerSystem>().Select(i=>i.Manufacturer).ToList().FirstOrDefault();
            Assert.AreEqual(manufacturer.ToString().ToUpper(), "DELL INC.");
        }

        [Test]
        public void Test_Win32_ComputerSystem_Error_Manufacturer()
        {

            var manufacturer = _system.GetEnumerator().AsEnumerable<ComputerSystem>().Select(i => i.Manufacturer).ToList().FirstOrDefault();
            Assert.AreNotEqual(manufacturer.ToString().ToUpper(), "APPLE");
        }

    }
}
