using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementLibrary
{
    public class Project_Excep:ApplicationException
    {
        public Project_Excep(string message) : base(message)
        {

        }
    }
//CASE 1:
    public class LoginExcep : ApplicationException
    {
        public LoginExcep(string message) : base(message)
        {

        }
    }

//CASE 2:
    public class NameException : ApplicationException
    {
        public NameException(string message) : base(message)
        {

        }

    }

    public class AgeException : ApplicationException
    {
        public AgeException(string message) : base(message)
        {

        }

    }

    public class DateInIndianFormatException : ApplicationException
    {
        public DateInIndianFormatException(string message) : base(message)
        {

        }

    }

    //CASE 3
    public class InvalidPatientIDException : ApplicationException
    {
        public InvalidPatientIDException(string message) : base(message)
        {

        }

    }
    public class InvalidSpecializationException : ApplicationException
    {
        public InvalidSpecializationException(string message) : base(message)
        {

        }

    }

    public class InvalidAppointmentIDException : ApplicationException
    {
        public InvalidAppointmentIDException(string message) : base(message)
        {

        }

    }
}
