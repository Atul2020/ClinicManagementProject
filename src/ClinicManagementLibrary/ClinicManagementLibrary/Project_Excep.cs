﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementLibrary
{
    
//CASE 1:
    public class InvalidLoginException : ApplicationException
    {
        public InvalidLoginException(string message) : base(message)
        {

        }
    }

//CASE 2:
    public class InvalidNameException : ApplicationException
    {
        public InvalidNameException(string message) : base(message)
        {

        }

    }

    public class InvalidAgeException : ApplicationException
    {
        public InvalidAgeException(string message) : base(message)
        {

        }

    }

    public class InvalidDateInIndianFormatException : ApplicationException
    {
        public InvalidDateInIndianFormatException(string message) : base(message)
        {

        }

    }
    public class InvalidDateNotInAvailableDatesException : ApplicationException
    {
        public InvalidDateNotInAvailableDatesException(string message) : base(message)
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

    public class InvalidCancellationException : ApplicationException
    {
        public InvalidCancellationException(string message) : base(message)
        {

        }

    }
}
