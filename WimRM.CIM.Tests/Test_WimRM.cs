using NUnit.Framework;
using System.Linq;
namespace WinRM.CIM.Tests
{
    [TestFixture]
    public class Test_WimRM
    {
        [Test]
        public void Test_No_Error_Query_Results()
        {
            var results = WinRM.Query("root\\cimv2", "SELECT * FROM Win32_Volume").ToList();
        }
           
    }
}
