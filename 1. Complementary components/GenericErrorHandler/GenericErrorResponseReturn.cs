using GenericErrorHandler.Interfaces;
using System;
using static System.Console;

namespace GenericErrorHandler
{
    public class GenericErrorResponse<T> : Error, IGenericErrorResponseReturn
    {
        public T ResponseItem;                   //This is the generic object response returned

        public GenericErrorResponse(int errCode = 0, string errMessage = "", string errTracking = "", T responseItem = default(T)) {
            SetErrorInfo(errCode, errMessage, errTracking);
            ResponseItem = responseItem;
        }

        public GenericErrorResponse() { }

        public override void ShowErrorMessageInConsole() {
            WriteLine($"Item to be returned: {ResponseItem.GetType()}");
            WriteLine($"Error Code: {_errorId}");
            WriteLine($"Error Description: {_errorMessage}");
            WriteLine($"Error Stack Trace: {_errorTracking}");
        }
    }
}
