using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementLibrary;
using System.Data.SqlClient;
namespace ClinicManagementClient
{
    public class ClinicClient
    {
        static void Main()
        {
            Console.WriteLine("**********CLINIC MANAGEMENT SYSTEM***********");
            Console.WriteLine();
            while (true)
            {   
                Console.WriteLine("Login Here");
                Console.WriteLine();
                Console.WriteLine("Enter your Username");
                string username = Console.ReadLine();
                Console.WriteLine("Enter the Password");
                string password = Console.ReadLine();
                Clinic clinic = new Clinic();
                try
                {
                    clinic.loginUser(username, password);
                    Console.WriteLine("Successfully Logged in");
                    Console.WriteLine();

                    IClinic ic = new Clinic();
                    while (true)
                    {
                        Console.WriteLine("1.View Doctors");
                        Console.WriteLine("2.Add Patient");
                        Console.WriteLine("3.Schedule Appointment");
                        Console.WriteLine("4.Cancel Appointment");
                        Console.WriteLine("5.Logout");
                        Console.WriteLine();
                        Console.WriteLine("Enter your Choice :");
                        int choice = int.Parse(Console.ReadLine());
                        if (choice == 5)
                        {
                            break;
                        }
                        switch (choice)
                        {
                            case 1:
                                {
                                    Console.WriteLine("***********View Doctors*************");
                                    List<Doctor> doc_list = ic.viewDoctorDetails();
                                    foreach (Doctor i in doc_list)
                                    {
                                        Console.WriteLine("---------DOCTOR DETAIL---------");
                                        Console.WriteLine("Doctor id: " + i.doctorID + "\nFirstName: " + i.firstName + "\nLastName: " + i.lastName
                                            + "\nSex: " + i.sex + "\nSpecialization: " + i.specialization + "\nVisiting Time From: " + i.visitingTimeFrom
                                            + "\nVisiting Time To: " + i.visitingTimeTo);
                                        Console.WriteLine("---------------------------------");
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine("***********Adding Patient Details*************");
                                    Console.WriteLine("Enter the Patient First Name: ");
                                    string firstName = Console.ReadLine();
                                    Console.WriteLine("Enter the Patient Last Name: ");
                                    string lastName = Console.ReadLine();
                                    Console.WriteLine("Enter the Patient Sex: ");
                                    string sex = Console.ReadLine();
                                    Console.WriteLine("Enter the age: ");
                                    int age = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter the Date Of Birth: ");
                                    string dob = Console.ReadLine();
                                    try { 

                                    ic.validatePatientDetails(new Patient(firstName, lastName, sex, age, Convert.ToDateTime(dob)), dob);
                                                              
                                        int id;
                                        DateTime dateofbirth = DateTime.Parse(dob);
                                        int ans = ic.addPatientDetails(new Patient(firstName, lastName, sex, age, dateofbirth), out id);
                                        if (ans == 1)
                                        {
                                            Console.WriteLine("Patient " + firstName + "" + lastName + "was inserted successfully!!!");
                                            Console.WriteLine("The Patient ID is : " + id);
                                        }
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    
                                    Console.WriteLine("-----------------------------");
                                    break;
                                }

                            case 3:
                                {
                                    Console.WriteLine("***********SCHEDULE APPOINTMENT*************");
                                    Console.WriteLine("Enter the patient id:");
                                    int patientID = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Enter the Specializations from the following: ");
                                    Console.WriteLine("General  \nInternal Medicine  \nPediatrics  \nOrthopedics  \nOphthalmology");
                                    string specialization = Console.ReadLine();
                                    try { 
                                    ic.validatePatIDSpecialization(patientID, specialization);
                                    
                                        List<Doctor> dc = ic.displayDoctorBySpecialization(specialization);
                                        Console.WriteLine("---------DOCTOR DETAIL BY SPECIALIZATION---------");
                                        foreach (Doctor i in dc)
                                        {
                                            Console.WriteLine("Doctor id: " + i.doctorID + "\nFirstName: " + i.firstName + "\nLastName: " + i.lastName
                                                   + "\nSex: " + i.sex + "\nSpecialization: " + i.specialization + "\nVisiting Time From: " + i.visitingTimeFrom
                                                   + "\nVisiting Time To: " + i.visitingTimeTo);
                                            Console.WriteLine("---------------------------------");
                                        }
                                        Console.WriteLine("Enter the date that you want to book appointment from the below dates: ");
                                        Console.WriteLine("26/08/2022 , 27/08/2022 , 28/08/2022");
                                        string visit_date = Console.ReadLine();
                                        ic.validateDatePresentInAvailableDates(visit_date);
                                        ic.validateDateInIndianFormat(visit_date);
                                            Console.WriteLine("Enter the Doctor Id: ");
                                            int doctorID = int.Parse(Console.ReadLine());
                                            DateTime date_of_visit = Convert.ToDateTime(visit_date);

                                            List<Appointment> slotlist = ic.displayTimeSlotsOfDoctor(doctorID, date_of_visit);
                                        List<int> aptIDList = new List<int>();
                                            foreach (Appointment i in slotlist)
                                            {
                                                Console.WriteLine("Appointment id: " + i.aptID + "\nDoctor ID: " + i.doctor_id + "\nVisiting Date: " + i.visiting_date
                                                       + "\nSlot Time: " + i.timeslot + "\nAppointment Status: " + i.apt_status + "\nPatient ID: " + i.patient_id);
                                                Console.WriteLine("---------------------------------");
                                            aptIDList.Add(i.aptID);

                                            }

                                            Console.WriteLine("Enter the appointment ID: ");
                                            int aptID = int.Parse(Console.ReadLine());

                                            ic.validateAppointmentID(aptID, aptIDList);
                                            ic.appointmentBooking(aptID, patientID);
                                        Console.WriteLine("The booking has been succesful and the patient id is: " + patientID);
    
                                    }
                                    catch(InvalidPatientIDException e) //success
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(InvalidSpecializationException e) //success
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(DateInIndianFormatException e) //fail
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(DateNotInAvailableDatesException e) //success
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(InvalidAppointmentIDException e) //success
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(Exception e)
                                    {
                                       Console.WriteLine(e.Message);
                                    }

                                    break;
                                }
                            case 4:
                                {
                                    Console.WriteLine("*********Cancel Appointment*************");
                                    Console.WriteLine("Enter the Patient ID:");
                                    int patient_id = int.Parse(Console.ReadLine());
                                    try{
                                        ic.validatePatientID(patient_id);
                                    Console.WriteLine("The Available dates for cancellation are: ");
                                    Console.WriteLine("26/08/2022 , 27/08/2022 , 28/08/2022");
                                   
                                        Console.WriteLine("Enter the Date: ");
                                        string visit_date = Console.ReadLine();
                                        ic.validateDatePresentInAvailableDates(visit_date);
                                         ic.validateDateInIndianFormat(visit_date);
                                            DateTime cancellation_date = DateTime.Parse(visit_date);
                                            List<Appointment> appointment_list = ic.displayPatientAppointmentsBooked(patient_id, cancellation_date);
                                            if (appointment_list.Count == 0)
                                            {
                                                Console.WriteLine("You dont have any active appointments!!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("**********THE BOOKED APPOINTMENTS ARE*************");
                                            List<int> aptIDList = new List<int>();
                                                foreach (Appointment i in appointment_list)
                                                {
                                                    Console.WriteLine("Appointment id: " + i.aptID + "\nDoctor ID: " + i.doctor_id + "\nVisiting Date: " + i.visiting_date
                                                           + "\nSlot Time: " + i.timeslot + "\nAppointment Status: " + i.apt_status + "\nPatient ID: " + i.patient_id);
                                                    Console.WriteLine("---------------------------------");
                                                    aptIDList.Add(i.aptID);
                                                }
                                                Console.WriteLine();
                                                Console.WriteLine("Enter the Appointment id which has to be cancelled: ");
                                                int aptID = int.Parse(Console.ReadLine());
                                                ic.validateAppointmentID(aptID, aptIDList);
                                                 ic.cancelBookedAppointment(aptID,patient_id);

                                            Console.WriteLine("The appointment with the id " + aptID + " has been cancelled successfully!!");

                                            }
                                        
                                    }
                                    catch(InvalidPatientIDException e) //success
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch (DateInIndianFormatException e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch (DateNotInAvailableDatesException e) //success
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(InvalidAppointmentIDException e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }

                                    break;
                                }
                            default:
                                {
                                    break;
                                }

                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
