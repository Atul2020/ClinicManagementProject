using ClinicManagementLibrary;
using System.Data.SqlClient;
namespace TestProject
{
    public class Tests
    {
        public static Login l;
        [SetUp]
        public void Setup()
        {
            l=new Login();
        }

        [Test]
        public void LoginTest()
        {
            bool actualvalue = l.loginUser("Atulacc", "07@atul");
            bool expectedvalue = true;
            Assert.AreEqual(expectedvalue, actualvalue);
            
        }
    }
}