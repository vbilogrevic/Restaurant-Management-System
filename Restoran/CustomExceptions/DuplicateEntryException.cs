﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.CustomExceptions
{
    public class DuplicateEntryException : Exception
    {

        public DuplicateEntryException() { }


        public DuplicateEntryException(string message) : base(message) { }

        public DuplicateEntryException(string message, Exception innerException)
            : base(message, innerException) { }

        
        public DuplicateEntryException(Exception innerException)
            : base("An error occurred", innerException) { }
    }
}
