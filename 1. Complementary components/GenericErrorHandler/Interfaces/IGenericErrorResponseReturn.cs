namespace GenericErrorHandler.Interfaces
{
    public interface IGenericErrorResponseReturn
    {
        void SetErrorInfo(int errCode, string errMessage, string errTracking);
        void ShowErrorMessageInConsole();
    }
}