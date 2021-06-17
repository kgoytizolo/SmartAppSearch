using System;
using System.Collections.Generic;

namespace SmartAppCore.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValidationErrors { get; set; }

        public ValidationException()
        {

        }
    }
}
