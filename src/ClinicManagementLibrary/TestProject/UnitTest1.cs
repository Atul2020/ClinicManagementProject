using ClinicManagementLibrary;
using System.Data.SqlClient;

// Class that contains testing for all the methods in Clinic.cs
namespace TestProject
{
    public class Tests
    {
        public static Clinic c;
        [SetUp]
        public void Setup()
        {
            c=new Clinic();
        }

        [Test]
        public void testLoginUser()
        {
            bool actualvalue = c.loginUser("Atulacc", "07@atul");
            bool expectedvalue = true;
            Assert.AreEqual(expectedvalue, actualvalue);
            
        }

        [Test]
        public void testViewDoctorDetails()
        {
            int actualvalue = c.viewDoctorDetails().Count;
            int expectedvalue = 5;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testValidatePatientDetails()
        {
            DateTime dt = Convert.ToDateTime("10/12/2000");
            Patient p = new Patient("Harry", "Maguire", "M", 45,dt);
            bool actualvalue = c.validatePatientDetails(p,"08/11/2000");
            bool expectedvalue = true;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testValidateDateInIndianFormat()
        {
            bool actualvalue = c.validateDateInIndianFormat("31/08/2022");
            bool expectedvalue = false;
            Assert.AreEqual(expectedvalue, actualvalue);

        }
        [Test]
        public void testDisplayDoctorBySpecialization()
        {
            int actualvalue = c.displayDoctorBySpecialization("General").Count;
            int expectedvalue = 1;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testValidateDoctorIDBySpecialization()
        {
            List<int> docIDList=new List<int>() { 10001,10002};
            bool actualvalue = c.validateDoctorIDBySpecialization(10001, docIDList);
            bool expectedvalue = true;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testDisplayTimeSlotsOfDoctor()
        {
            int actualvalue = c.displayTimeSlotsOfDoctor(10001,Convert.ToDateTime("29/08/2022")).Count();
            int expectedvalue = 2;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testAppoinmentBooking()
        {
            int actualvalue = c.appointmentBooking(200, 100);
            int expectedvalue = 1;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testAddPatientDetails()
        {
            int actualvalue = c.addPatientDetails(new Patient("Sam", "Samson", "M", 20,DateTime.Parse("11/07/2000")),out int patient_id);
            int expectedvalue = 1;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testViewPatientDetails()
        {
            int actualvalue = c.viewPatientDetails().Count;
            int expectedvalue = 1;
            Assert.AreEqual(expectedvalue, actualvalue);

        }

        [Test]

        public void testValidatePatIDSpecialization()
        {
            bool actualvalue = c.validatePatIDSpecialization(100, "General");
            bool expectedvalue = true;
            Assert.AreEqual(expectedvalue, actualvalue);
        }


        [Test]
        public void testDisplayAppointmentsBooked()
        {
            int actualvalue = c.displayPatientAppointmentsBooked(1,Convert.ToDateTime("26/08/2022")).Count();
            int expectedvalue = 0;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testCancelBookedAppointment()
        {
            int actualvalue = c.cancelBookedAppointment(216,100);
            int expectedvalue = 1;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testValidPatientID()
        {
            bool actualvalue = c.validatePatientID(100);
            bool expectedvalue = true;
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [Test]
        public void testValidateDatePresentInAvailableDates()
        {
            bool actualvalue = c.validateDatePresentInAvailableDates("29/08/2022");
            bool expectedvalue = true;
            Assert.AreEqual(expectedvalue, actualvalue);
        }
        
    }
}