using ClinicManagementLibrary;
using System.Data.SqlClient;
namespace TestProject
{
    public class Tests
    {
        public static Login l;
        public static HomePage hp;
        [SetUp]
        public void Setup()
        {
            l=new Login();
            hp=new HomePage();
        }

        [Test]
        public void loginTest()
        {
            bool actualvalue = l.loginUser("Atulacc", "07@atul");
            bool expectedvalue = true;
            Assert.AreEqual(expectedvalue, actualvalue);
            
        }

        [Test]
        public void viewDetailsTest()
        {
            int actualvalue = hp.viewDoctorDetails().Count;
            int expectedvalue = 5;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void validateDetailsTest()
        {
            DateTime dt = Convert.ToDateTime("10/12/2000");
            Patient p = new Patient(101, "Harry", "Maguire", "M", 45,dt);
            bool actualvalue = hp.validatePatientDetails(p,"08/11/2000");
            bool expectedvalue = true;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testDisplayDocBySpec()
        {
            int actualvalue = hp.displayDoctorBySpecialization("General").Count;
            int expectedvalue = 1;
            Assert.AreEqual(expectedvalue, actualvalue);
        }
    }
}