using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementLibrary
{
    public interface IHomepage
    {
         List<Doctor> viewDoctorDetails();

        void addPatientDetails(Patient p);

        bool validatePatientDetails(Patient p,string dob);

        List<Doctor> displayDoctorBySpecialization(string spec);
    }
}
