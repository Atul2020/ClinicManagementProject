using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementLibrary
{
    public class Doctor
    {
        public int doctorID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string sex { get; set; }
        public string specialization { get; set; }
        public TimeSpan visitingTimeFrom { get; set; }
        public TimeSpan visitingTimeTo { get; set; }

        public Doctor(){}
        public Doctor(int doctorID,string firstName,
        string LastName,string sex,string specialization,TimeSpan visitingTimeFrom, TimeSpan visitingTimeTo)
        {
            this.doctorID = doctorID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.sex = sex;
            this.specialization = specialization;
            this.visitingTimeFrom = visitingTimeFrom;
            this.visitingTimeTo = visitingTimeTo;
        }
    }
}
