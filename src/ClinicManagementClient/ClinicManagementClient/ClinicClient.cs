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
                Console.WriteLine();
                Console.WriteLine("Welcome to the Login Page");
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
                        Console.WriteLine("***********MENU*************");
                        Console.WriteLine("1.View Doctors");
                        Console.WriteLine("2.Add Patient");
                        Console.WriteLine("3.View Patients");
                        Console.WriteLine("4.Schedule Appointment");
                        Console.WriteLine("5.Cancel Appointment");
                        Console.WriteLine("6.Logout");
                        Console.WriteLine();
                        Console.WriteLine("Enter your Choice :");
                        int choice = int.Parse(Console.ReadLine());
                        if (choice == 6)
                        {
                            break;
                        }
                        switch (choice)
                        {
                            case 1: // HERE ALL THE AVAILABLE DOCTOR'S DETAILS ARE DISPLAYED
                                {
                                    Console.WriteLine("***********VIEW DOCTORS*************");
                                    List<Doctor> doc_list = ic.viewDoctorDetails();
                                    foreach (Doctor i in doc_list)
                                    {
                                        Console.WriteLine("***********DOCTOR DETAILS*************");
                                        Console.WriteLine("Doctor id: " + i.doctorID + "\nFirstName: " + i.firstName + "\nLastName: " + i.lastName
                                            + "\nSex: " + i.sex + "\nSpecialization: " + i.specialization + "\nVisiting Time From: " + i.visitingTimeFrom
                                            + "\nVisiting Time To: " + i.visitingTimeTo);
                                        Console.WriteLine("---------------------------------");
                                    }
                                    break;
                                }
                            case 2: // HERE THE PATIENT DETAILS ENTERED ARE STORED IN THE DATABASE
                                {
                                    try 
                                    { 
                                    Console.WriteLine("***********ADDING PATIENT DETAILS*************");
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
                                    
                                        
                                        ic.validatePatientDetails(new Patient(firstName, lastName, sex, age, Convert.ToDateTime(dob)));
                                        ic.validateDateInIndianFormat(dob);                     
                                        int id;
                                        DateTime dateofbirth = DateTime.Parse(dob);
                                        int ans = ic.addPatientDetails(new Patient(firstName, lastName, sex, age, dateofbirth), out id);
                                        if (ans == 1)
                                        {
                                            Console.WriteLine("Patient " + firstName + " " + lastName + " was inserted successfully!!!");
                                            Console.WriteLine("The Patient ID is : " + id);
                                        }
                                    }
                                    catch(InvalidNameException e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(InvalidAgeException e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(InvalidDateInIndianFormatException e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(FormatException e)
                                    {
                                        Console.WriteLine("The date entered should be in dd/mm/yyyy format");
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    
                                    Console.WriteLine("-----------------------------");
                                    break;
                                }
                            case 3: // HERE ALL THE PATIENTS IN THE PATIENT'S TABLE ARE DISPLAYED
                                {
                                    Console.WriteLine("***********VIEW PATIENTS*************");
                                    List<Patient> pat_list = ic.viewPatientDetails();
                                    foreach (Patient i in pat_list)
                                    {
                                        Console.WriteLine("***********PATIENT DETAILS*************");
                                        Console.WriteLine("Patient ID: " + i.patientID + "\nFirstName: " + i.firstName + "\nLastName: " + i.lastName
                                            + "\nSex: " + i.sex + "\nAge: " + i.age + "\nDate Of Birth: " + i.dob);
                                        Console.WriteLine("---------------------------------");
                                    }
                                    break;
                                }
                            case 4: // HERE THE APPOINMENT BOOKING IS DONE, FIRST DOCTOR DETAILS BY SPECIALIZATION ARE DISPLAYED
                                    // AND BASED ON THE DATE, THE AVAILABLE TIME SLOTS ARE DISPLAYED FROM WHICH THE BOOKING IS DONE
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
                                        List<int> doctorIDList = new List<int>();

                                        Console.WriteLine("***********DOCTOR DETAIL BY SPECIALIZATION*************");
                                        foreach (Doctor i in dc)
                                        {
                                            Console.WriteLine("Doctor id: " + i.doctorID + "\nFirstName: " + i.firstName + "\nLastName: " + i.lastName
                                                   + "\nSex: " + i.sex + "\nSpecialization: " + i.specialization + "\nVisiting Time From: " + i.visitingTimeFrom
                                                   + "\nVisiting Time To: " + i.visitingTimeTo);
                                            Console.WriteLine("---------------------------------");
                                            doctorIDList.Add(i.doctorID);
                                        }
                                        
                                        Console.WriteLine("Enter the date that you want to book appointment from the below dates: ");
                                        Console.WriteLine("29/08/2022 , 30/08/2022 , 31/08/2022, 01/09/2022, 02/09/2022, 03/09/2022, 04/09/2022, 05/09/2022, 06/09/2022");
                                        string visit_date = Console.ReadLine();

                                        ic.validateDatePresentInAvailableDates(visit_date);
                                        ic.validateDateInIndianFormat(visit_date);

                                        Console.WriteLine("Enter the Doctor Id: ");
                                        int doctorID = int.Parse(Console.ReadLine());
                                        ic.validateDoctorIDBySpecialization(doctorID, doctorIDList);

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
                                         Console.WriteLine("The booking has been successfully done!!!");
    
                                    }
                                    catch(InvalidPatientIDException e) 
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(InvalidSpecializationException e) 
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch (InvalidDateInIndianFormatException e) 
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch(InvalidDateNotInAvailableDatesException e) 
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
                            case 5: // HERE BASED ON THE PATIENT_ID AND THE DATE ENTERED, THE APPOINTMENTS BOOKED ARE DISPLAYED. THEN THE APPOINTMENT CAN BE CANCELLED.
                                {
                                    Console.WriteLine("***********CANCEL APPOINTMENT*************");
                                    Console.WriteLine("Enter the Patient ID:");
                                    int patient_id = int.Parse(Console.ReadLine());

                                    try{
                                        ic.validatePatientID(patient_id);
                                        Console.WriteLine("The Available dates for cancellation are: ");
                                        Console.WriteLine("29/08/2022 , 30/08/2022 , 31/08/2022, 01/09/2022, 02/09/2022, 03/09/2022, 04/09/2022, 05/09/2022, 06/09/2022");
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
                                          Console.WriteLine("***********THE BOOKED APPOINTMENTS ARE*************");
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
                                    catch(InvalidPatientIDException e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch (InvalidDateInIndianFormatException e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch (InvalidDateNotInAvailableDatesException e)
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
                                    Console.WriteLine("Please Enter the correct Choice !!");
                                    break;
                                }

                        }
                    }
                }
                catch(InvalidLoginException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
