using System;

namespace SmartAppCore.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object item): base($"{name} - {item} has not been found")
        {

        }
    }
}
