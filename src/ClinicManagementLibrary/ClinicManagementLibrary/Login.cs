using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ClinicManagementLibrary
{
    public class Login : ILogin
    {
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlConnection getcon()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ClinicManagement;Integrated Security=true");
            con.Open();
            return con;
        }
        public bool loginUser(string username, string password)
        {
            con= getcon();
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
                return false;
            }
        }
        public void addPatient()
        {
            throw new NotImplementedException();
        }

        public void cancelAppointment()
        {
            throw new NotImplementedException();
        }


        public void scheduleAppointment()
        {
            throw new NotImplementedException();
        }

        public SqlDataReader viewDocotors()
        {
            throw new NotImplementedException();
        }
    }
}
