using System;

namespace SmartAppCore.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message): base(message)
        {

        }
    }
}
