using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.CustomExceptions
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException() { }


        public InvalidValueException(string message) : base(message) { }

        public InvalidValueException(string message, Exception innerException)
            : base(message, innerException) { }


        public InvalidValueException(Exception innerException)
            : base("An error occurred", innerException) { }
    }
}
