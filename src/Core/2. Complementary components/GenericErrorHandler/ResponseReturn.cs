using GenericErrorHandler.Interfaces;
using System;
using static System.Console;

namespace GenericErrorHandler
{
    public class Response<T> : Error, IGenericErrorResponseReturn
    {
        public T ResponseItem;                   //This is the generic object response returned
        public bool Success { get; set; }        //Response indicates if returned transaction was OK or not

        public Response(int errCode = 0, string errMessage = "", string errTracking = "", T responseItem = default(T)) {
            SetErrorInfo(errCode, errMessage, errTracking);
            ResponseItem = responseItem;
        }

        public Response() { }

        public override void SetErrorInfo(int errCode = 0, string errMessage = "", string errTracking = "")
        {
            _errorId = errCode;
            _errorMessage = errMessage;
            _errorTracking = errTracking;
            if(errCode == 0) Success = true;              
        }

        public override void ShowErrorMessageInConsole() {
            WriteLine($"Item to be returned: {ResponseItem.GetType()}");
            WriteLine($"Error Code: {_errorId}");
            WriteLine($"Error Description: {_errorMessage}");
            WriteLine($"Error Stack Trace: {_errorTracking}");
        }
    }
}
