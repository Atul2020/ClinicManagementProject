using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
namespace ClinicManagementLibrary
{
    public class Clinic : IClinic
    {
        List<Doctor> docList = new List<Doctor>();

        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlCommand cmd1;

        

        public static SqlConnection getcon()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ClinicManagement;Integrated Security=true");
            con.Open();
            return con;
        }

 //1. User login method
        public bool loginUser(string username, string password)
        {
            con = getcon();
            SqlCommand cmd = new SqlCommand("select * from Users where username=@username and password=@password");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                return true;
            }
            else
            {
                throw new InvalidLoginException("The login has failed!! Please enter the correct credentials!!");
            }
        }

//2.Home
        //Method to view the doctor details
        public List<Doctor> viewDoctorDetails()
        {
            con = getcon();
            SqlCommand cmd = new SqlCommand("select * from doctors");
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Doctor d = new Doctor();
                d.doctorID = Convert.ToInt32(dr["doctor_id"]);
                d.firstName = Convert.ToString(dr["firstname"]);
                d.lastName = Convert.ToString(dr["lastName"]);
                d.sex = Convert.ToString(dr["sex"]);
                d.specialization = Convert.ToString(dr["specialization"]);
                d.visitingTimeFrom =(TimeSpan)dr["visiting_hours_from"];
                d.visitingTimeTo = (TimeSpan)dr["visiting_hours_to"];
                docList.Add(d);
            }
            return docList;
        }

        //Method that validates the patient details(firstname,lastname and age)
        public bool validatePatientDetails(Patient p,string dob)
        {
            string fname = p.firstName;
            string lname = p.lastName;
           
            int age = p.age;
            bool flag = true;
            bool flag1 = true;
            bool flag2 = true;
            Regex re = new Regex("[^a-zA-Z0-9]");
            if (re.IsMatch(fname) | re.IsMatch(lname))
            {
                flag = false;
            }

            if (age<0 | age > 120)
            {
                flag1 = false;
            }

            DateTime d;

            bool dateValidity = DateTime.TryParseExact(dob,"dd/mm/yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None,out d);
            if (dateValidity==true)
            {
                flag2 = true;
            }
            else
            {
                flag2 =false;
            }
            
            if (flag == false)
            {
                throw new InvalidNameException("The name entered should not contain special characters");
            }
            if (flag1 == false)
            {
                throw new InvalidAgeException("The age entered should be between 0 and 121");
            }
            if (flag2 == false)
            {
                throw new InvalidDateInIndianFormatException("The date entered should should be in dd/mm/yyyy format");
            }
            return true;
        }

        //Adding the patient details into the patient table
        public int addPatientDetails(Patient p,out int patientID)
        {
            con = getcon();
            cmd = new SqlCommand("insert into patients(firstname,lastname,sex,age,dob) values(@firstname,@lastname,@sex,@age,@dob)");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@firstname", p.firstName);
            cmd.Parameters.AddWithValue("@lastname", p.lastName);
            cmd.Parameters.AddWithValue("@sex", p.sex);
            cmd.Parameters.AddWithValue("@age", p.age);
            cmd.Parameters.AddWithValue("@dob", p.dob);
            int i = cmd.ExecuteNonQuery();
            cmd1 = new SqlCommand("Select * from patients where firstname=@firstname and lastname=@lastname and sex=@sex and age=@age and dob=@dob",con);
 
            cmd1.Parameters.AddWithValue("@firstname", p.firstName);
            cmd1.Parameters.AddWithValue("@lastname", p.lastName);
            cmd1.Parameters.AddWithValue("@sex", p.sex);
            cmd1.Parameters.AddWithValue("@age", p.age);
            cmd1.Parameters.AddWithValue("@dob", p.dob);
            SqlDataReader dr = cmd1.ExecuteReader();
            dr.Read();
            patientID = dr.GetInt32(0);
            return i;

        }

        public List<Patient> viewPatientDetails()
        {
            List<Patient> patientDetails = new List<Patient>();
            con = getcon();
            SqlCommand cmd = new SqlCommand("select * from patients");
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Patient p = new Patient();
                p.patientID = Convert.ToInt32(dr["patient_id"]);
                p.firstName = Convert.ToString(dr["firstname"]);
                p.lastName = Convert.ToString(dr["lastName"]);
                p.sex = Convert.ToString(dr["sex"]);
                p.age = Convert.ToInt32(dr["age"]);
                p.dob = Convert.ToDateTime(dr["dob"]);
            
                patientDetails.Add(p);
            }
            return patientDetails;
        }

        //3.schedule appointment
        public bool validateDateInIndianFormat(string date)
        {
  
            DateTime d;
            bool dateValidity = DateTime.TryParseExact(date, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
            out d);
            if (dateValidity == false)
            {
                throw new InvalidDateInIndianFormatException("The date entered should be in dd/mm/yyyy format");
            }
            
            return true;
        }


        //Validating the pat_id and the specialization
        public bool validatePatIDSpecialization(int patient_id,string specialization)
        {
            List<string> s = new List<string>() { "General" , "Internal Medicine" , "Pediatrics" , "Orthopedics" , "Ophthalmology" };
            bool flag = false;
            bool flag1 = false;
            con = getcon();
            SqlCommand cmd = new SqlCommand("select * from patients where patient_id=@patient_id");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@patient_id", patient_id);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                flag = true;
            }
            foreach(string i in s)
            {
                if (i == specialization)
                {
                    flag1 = true;
                    break;
                }
            }
            if (flag == false)
            {
                throw new InvalidPatientIDException("The Patient ID you have entered is Invalid!!! ");
            }
            if (flag1 == false)
            {
                throw new InvalidSpecializationException("The specialization entered is Invalid!!!");
            }
            return true;
        }
       

       //Display all the doctors based on specializtion required
        public List<Doctor> displayDoctorBySpecialization(string specialization)
        {

            List<Doctor> doctorsDetails = new List<Doctor>();
            con = getcon();
            SqlCommand cmd = new SqlCommand("select * from doctors where specialization=@specialization");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@specialization", specialization);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Doctor d = new Doctor();
                d.doctorID = Convert.ToInt32(dr["doctor_id"]);
                d.firstName = Convert.ToString(dr["firstname"]);
                d.lastName = Convert.ToString(dr["lastName"]);
                d.sex = Convert.ToString(dr["sex"]);
                d.specialization = Convert.ToString(dr["specialization"]);
                d.visitingTimeFrom = (TimeSpan)dr["visiting_hours_from"];
                d.visitingTimeTo = (TimeSpan)dr["visiting_hours_to"];
                doctorsDetails.Add(d);
            }
            return doctorsDetails;
        }

        //validates if the doctor id entered is matching
        public bool validateDoctorIDBySpecialization(int doctorID, List<int> doctorIDList)
        {
            bool flag = false;
            foreach(int i in doctorIDList)
            {
                if (doctorID == i)
                {
                    flag = true;
                    break;
                }
            }
            if (flag == false)
            {
                throw new InvalidDoctorIDException("There was no doctor matching the specialization with the doctor ID entered. Please enter the ID again!!");
            }
            return true;
        }
        //displays the available time slots that are available
        public List<Appointment> displayTimeSlotsOfDoctor(int docID, DateTime dateOfAppointment)
        {
            List<Appointment> slotList = new List<Appointment>();
            con =getcon();
            SqlCommand cmd = new SqlCommand("select * from appointments where doctor_id=@doctor_id and apt_status='free' and visiting_date=@visiting_date");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@doctor_id",docID);
            cmd.Parameters.AddWithValue("@visiting_date",dateOfAppointment);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                slotList.Add(new Appointment(dr.GetInt32(0), dr.GetInt32(1),dr.GetDateTime(2), dr.GetString(3), dr.GetString(4)));
            }
            return slotList;
        }
        //Validates the appointment id entered by the user
        public bool validateAppointmentID(int aptID,List<int> aptIDList)
        {
            bool flag =false;
            foreach(int apt in aptIDList)
            {
                if (aptID == apt)
                {
                    flag = true;
                    break;
                }
            }
            if (flag == false)
            {
                throw new InvalidAppointmentIDException("The Appointment ID entered is  Invalid!!!");
            }
            return true;
        }
        //Method to book the appointment
        public int appointmentBooking(int apt_id,int patient_id)
        {
            con = getcon();
            SqlCommand cmd = new SqlCommand("update appointments set apt_status='booked',patient_id=@patient_id where aptID=@aptID",con);
            cmd.Parameters.AddWithValue("@aptID", apt_id);
            cmd.Parameters.AddWithValue("@patient_id",patient_id);
            int i = cmd.ExecuteNonQuery();
            if (i == 0)
            {
                throw new InvalidAppointmentIDException("The booking was not done successfully!! The Appointment ID is not Valid!!!");
            }
            return i;
        }

//4.CancellingAppointment
        
        //Validates the patientID
        public bool validatePatientID(int patient_id)
        {
            bool flag = false;
            con = getcon();
            SqlCommand cmd = new SqlCommand("select * from patients where patient_id=@patient_id");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@patient_id",patient_id);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                return true;
            }
            throw new InvalidPatientIDException("The patient id entered is not valid");
        }

        //Displays all the appointments matching the patient_id
        public List<Appointment> displayPatientAppointmentsBooked(int patient_id,DateTime visit_date)
        {
            List<Appointment> app = new List<Appointment>();
            con = getcon();
            SqlCommand cmd = new SqlCommand("select * from appointments where patient_id=@patient_id and visiting_date=@visiting_date",con);
            cmd.Parameters.AddWithValue("@patient_id", patient_id);
            cmd.Parameters.AddWithValue("@visiting_date", visit_date);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                app.Add(new Appointment(dr.GetInt32(0),dr.GetInt32(1),dr.GetDateTime(2),dr.GetString(3),dr.GetString(4),dr.GetInt32(5)));
            }
            return app;
        }

        //Appointment cancellation is done
        public int cancelBookedAppointment(int apt_id,int patient_id)
        {
            con = getcon();
            SqlCommand cmd = new SqlCommand("update appointments set apt_status='free',patient_id=null where aptID=@aptID and patient_id=@patient_id", con);
            cmd.Parameters.AddWithValue("@aptID", apt_id);
            cmd.Parameters.AddWithValue("@patient_id", patient_id);
            int i = cmd.ExecuteNonQuery();
            if (i == 0)
            {
                throw new InvalidCancellationException("The cancellation has not been successful");
            }
            return i;

        }
        // Validates if the date entered is present in the date list
        public bool validateDatePresentInAvailableDates(string visit_date)
        {
            bool flag = false;
            List<string> validDates = new List<string>() { "29/08/2022", "30/08/2022", "31/08/2022", "01/09/2022", "02/09/2022",
                "03/09/2022","04/09/2022","05/09/2022","06/09/2022" };
            foreach(string i in validDates)
            {
                if (i == visit_date)
                {
                    flag = true;
                    break;
                }
            }
            if (flag ==false)
            {
                throw new InvalidDateNotInAvailableDatesException("The date entered is not part of the available dates");
            }
            return true;
        }
    }
}
