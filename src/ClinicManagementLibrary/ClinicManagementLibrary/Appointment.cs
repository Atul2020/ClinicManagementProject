using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Appointment Class with its Constructors

namespace ClinicManagementLibrary
{
    public class Appointment
    {
        public int aptID { get; set; } 
        public int doctor_id { get; set; }

        public DateTime visiting_date { get; set; }
        public string timeslot { get; set; }
        public string apt_status { get; set; }

        public int? patient_id { get; set; }

        public Appointment() { }

        public Appointment(int aptID, int doctor_id,DateTime visiting_date, string timeslot, string apt_status)
        {
            this.aptID = aptID;
            this.doctor_id = doctor_id;
            this.visiting_date = visiting_date;
            this.timeslot = timeslot;
            this.apt_status = apt_status;
            this.patient_id = null;
        }
        public Appointment(int aptID, int doctor_id,DateTime visiting_date, string timeslot, string apt_status, int? patient_id)
        {
            this.aptID = aptID;
            this.doctor_id = doctor_id;
            this.visiting_date = visiting_date;
            this.timeslot = timeslot;
            this.apt_status = apt_status;
            this.patient_id = patient_id;
        }
        
    }
}
