using System;

namespace SmartAppCore.Exceptions
{
    public class InternalServiceException : ApplicationException
    {
        public InternalServiceException(string message): base(message)
        {

        }
    }
}
