using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementLibrary
{
    public class User
    {
        string username { get; set; }
        string password { get; set; }
        string firstName { get; set; }
        string lastName { get; set; }
        
        public User()
        {
        }
        public User(string username, string password, string firstName, string lastName)
        {
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }
}
