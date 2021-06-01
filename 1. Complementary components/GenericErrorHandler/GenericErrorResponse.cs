using GenericErrorHandler.Interfaces;

namespace GenericErrorHandler
{
    public class GenericErrorResponse : Error, IGenericErrorResponse 
    {
        public GenericErrorResponse() { }
        public GenericErrorResponse(int errCode = 0, string errMessage = "", string errTracking = "") => SetErrorInfo(errCode, errMessage, errTracking);
    }
}