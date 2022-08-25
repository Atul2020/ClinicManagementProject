using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementLibrary
{
    public class Patient
    {
        public int patientID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public DateTime date { get; set; }
        
        public Patient() { }
        public Patient(int patientID, string firstName, string lastName, string sex, int age, DateTime date)
        {
            this.patientID = patientID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.sex = sex;
            this.age = age;
            this.date = date;
        }
    }
}
