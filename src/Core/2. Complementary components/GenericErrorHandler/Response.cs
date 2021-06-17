using GenericErrorHandler.Interfaces;

namespace GenericErrorHandler
{
    public class Response : Error, IGenericErrorResponse 
    {
        public Response() { }
        public Response(int errCode = 0, string errMessage = "", string errTracking = "") => SetErrorInfo(errCode, errMessage, errTracking);
    }
}