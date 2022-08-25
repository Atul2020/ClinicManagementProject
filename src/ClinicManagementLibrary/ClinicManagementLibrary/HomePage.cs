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
    public class HomePage : IHomepage
    {
        List<Doctor> docList = new List<Doctor>(); 
        public static SqlConnection con;
        public static SqlCommand cmd;

        public static SqlConnection getcon()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ClinicManagement;Integrated Security=true");
            con.Open();
            return con;
        }
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

            bool chValidity = DateTime.TryParseExact(dob,"dd/mm/yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None,
            out d);
            if (chValidity==true)
            {
                flag2 = true;
            }
            else
            {
                flag2 =false;
            }
            return flag && flag1 && flag2;

            
            
        }
        public void addPatientDetails(Patient p)
        {
            con = getcon();
            cmd = new SqlCommand("insert into patients values(@patient_id,@firstname,@lastname,@sex,@age,@date)");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@patient_id", p.patientID);
            cmd.Parameters.AddWithValue("@firstname", p.firstName);
            cmd.Parameters.AddWithValue("@lastname", p.lastName);
            cmd.Parameters.AddWithValue("@sex", p.sex);
            cmd.Parameters.AddWithValue("@age", p.age);
            cmd.Parameters.AddWithValue("@dob", p.date);
            int i = cmd.ExecuteNonQuery();
            Console.WriteLine(i + " number of Row(s) affected");
        }

        public List<Doctor> displayDoctorBySpecialization(string specialization)
        {

            List<Doctor> doctorsdetails = new List<Doctor>();
            con = getcon();
            SqlCommand cmd = new SqlCommand("select * from doctors where specialization=@specialization",con);
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
                doctorsdetails.Add(d);
            }
            return doctorsdetails;
        }



    }
}
