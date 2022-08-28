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

    // User login method
        public bool loginUser(string username, string password)
        {
            //The username and password entered by the user are verified with the data in "Users" table.
            //If the datareader is empty then an an InvalidLoginException is Thrown.
            con = getcon();
            SqlCommand cmd = new SqlCommand("sp_loginUser");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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

        //Method to view the doctor details
        public List<Doctor> viewDoctorDetails()
        {
            //The "Doctors" table is queried and the data is sent to the  client where it is printed.
            con = getcon();
            SqlCommand cmd = new SqlCommand("sp_viewDocDetails");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
        public bool validatePatientDetails(Patient p)
        {
            //The firstname and lastname are validated so that they dont have any special characters.
            //The age is validated so that it is in between 0 and 120.
            //Various exceptions are thrown if the validation is unsuccessful.
            string fname = p.firstName;
            string lname = p.lastName;
           
            int age = p.age;
            bool flag = true;
            bool flag1 = true;
            Regex re = new Regex("[^a-zA-Z0-9]");
            if (re.IsMatch(fname) | re.IsMatch(lname))
            {
                flag = false;
            }

            if (age<0 | age > 120)
            {
                flag1 = false;
            }

            if (flag == false)
            {
                throw new InvalidNameException("The name entered should not contain special characters");
            }
            if (flag1 == false)
            {
                throw new InvalidAgeException("The age entered should be between 0 and 121");
            }
            return true;
        }

        //Adding the patient details into the patient table
        public int addPatientDetails(Patient p,out int patientID)
        {
            //The details entered by the user are entered into patients table.
            //The out parameter is used so that this function can return another value to the client which is the patient_id.
            con = getcon();
            SqlCommand cmd = new SqlCommand("sp_insertIntoPatients");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
        // Method to view all the patient detials
        public List<Patient> viewPatientDetails()
        {
            //All the patient details are retrieved from the patients table and are returned as a list.
            List<Patient> patientDetails = new List<Patient>();
            con = getcon();
            SqlCommand cmd = new SqlCommand("sp_viewPatientDetails");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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

        // Method that validates date in Indian Format
        public bool validateDateInIndianFormat(string date)
        {
            //The date entered by the user is validated on whether it follows the Indian Format and this function returns
            //a boolean value true otherwise throws an exception.
            DateTime d;
            bool dateValidity = DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
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
            //The entered specialization by the user is checked and also the patientid.
            //If validation is not successful exceptions are thrown.
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
            //Based on the specialization the user enters,all the respective doctors details available are stored and returned as a list.
            List<Doctor> doctorsDetails = new List<Doctor>();
            con = getcon();
            SqlCommand cmd = new SqlCommand("sp_displayDocBySpecialization");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
            //This function validates whether the doctorID entered by the user is matching the doctor's available with
            //that specialization. Otherwise it throws an exception.
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
            //This function queries the appointments table for the appointments that are available to be booked for that respective doctor
            //and returns a list to the client.
            List<Appointment> slotList = new List<Appointment>();
            con =getcon();
            SqlCommand cmd = new SqlCommand("sp_displayAvailableTimeSlot");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
            //The appointment id is validated as to whether it is the correct appointment that is free to be booked.
            //Otherwise it throws an exception.
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
            //The booking is done here where the status is changed to booked and the patients_id is updated in the appointments table.
            //If the appointment id was invalid an exception is thrown.
            con = getcon();
            SqlCommand cmd = new SqlCommand("sp_bookAppointment");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@aptID", apt_id);
            cmd.Parameters.AddWithValue("@patient_id",patient_id);
            int i = cmd.ExecuteNonQuery();
            if (i == 0)
            {
                throw new InvalidAppointmentIDException("The booking was not done successfully!! The Appointment ID is not Valid!!!");
            }
            return i;
        }
        
        //Validates the patientID
        public bool validatePatientID(int patient_id)
        {
            //The patient id entered is validated and if its not present in the patients table and exception is thrown.
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
            //All of the appointments booked by a specific patient are displayed based on their patient id and visit_date.
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
            //The cancellation of an already booked appointment is done here.
            //If the cancellation was not successfull,then an exception is thrown.
            con = getcon();
            SqlCommand cmd = new SqlCommand("sp_cancelBookedAppointment");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = con;
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
            //The date entered by the user is verified as to whether it is present in the available dates list.
            //Otherwise an exception is thrown.
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
                throw new InvalidDateNotInAvailableDatesException("The date entered is not part of the available dates or the format of the date should be dd/mm/yyyy!!!");
            }
            return true;
        }
    }
}
