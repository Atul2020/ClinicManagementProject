using ClinicManagementLibrary;
using System.Data.SqlClient;

// Class that contains testing for all the methods in Clinic.cs
namespace TestProject
{
    public class Tests
    {
        public static Clinic hp;
        [SetUp]
        public void Setup()
        {
            hp=new Clinic();
        }

        [Test]
        public void loginTest()
        {
            bool actualvalue = hp.loginUser("Atulacc", "07@atul");
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
            Patient p = new Patient("Harry", "Maguire", "M", 45,dt);
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

        [Test]
        public void testAppointmentDetailsByDocID()
        {
            int actualvalue = hp.displayTimeSlotsOfDoctor(10002,Convert.ToDateTime("28/08/2022")).Count();
            int expectedvalue = 4;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testAppoinmentBooking()
        {
            int actualvalue = hp.appointmentBooking(200, 100);
            int expectedvalue = 1;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void addPatientDetails()
        {
            int actualvalue = hp.addPatientDetails(new Patient("Sam", "Samson", "M", 20,DateTime.Parse("11/07/2000")),out int patient_id);
            int expectedvalue = 1;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]

        public void validatePatIDSpecialization()
        {
            bool actualvalue = hp.validatePatIDSpecialization(100, "General");
            bool expectedvalue = true;
            Assert.AreEqual(expectedvalue, actualvalue);
        }


        [Test]
        public void testDisplayAppointmentsBooked()
        {
            int actualvalue = hp.displayPatientAppointmentsBooked(1,Convert.ToDateTime("26/08/2022")).Count();
            int expectedvalue = 0;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testCancelBooking()
        {
            int actualvalue = hp.cancelBookedAppointment(216,100);
            int expectedvalue = 1;
            Assert.AreEqual(expectedvalue, actualvalue);
        }
    }
}