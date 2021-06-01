namespace GenericErrorHandler.Interfaces
{
    public interface IGenericErrorResponse
    {
        void SetErrorInfo(int errCode, string errMessage, string errTracking);
        void ShowErrorMessageInConsole();
    }
}