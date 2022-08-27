using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Interface that contains the method definitions

namespace ClinicManagementLibrary
{
    public interface IClinic
    {
        bool loginUser(string username, string password);
        List<Doctor> viewDoctorDetails();

        bool validatePatientDetails(Patient p, string dob);
        int addPatientDetails(Patient p,out int patientID);

        List<Patient> viewPatientDetails();

        bool validateDateInIndianFormat(string date);
        bool validatePatIDSpecialization(int patient_id, string specialization);
                        
        List<Doctor> displayDoctorBySpecialization(string spec);
        bool validateDoctorIDBySpecialization(int doctorID, List<int> doctorIDList);

        List<Appointment> displayTimeSlotsOfDoctor(int docID,DateTime dateOfAppointment);

        bool validateAppointmentID(int aptID, List<int> aptIDList);

        int appointmentBooking(int aptID,int patient_id);

        bool validatePatientID(int patient_id);

        List<Appointment> displayPatientAppointmentsBooked(int patient_id, DateTime visit_date);

        int cancelBookedAppointment(int aptID,int patient_id);
        
        bool validateDatePresentInAvailableDates(string visit_date);

    }
}
