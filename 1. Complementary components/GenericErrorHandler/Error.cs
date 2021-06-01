using static System.Console;

namespace GenericErrorHandler
{
    public class Error
    {
        protected int _errorId;
        protected string _errorMessage;
        protected string _errorTracking;

        public Error(int errCode = 0, string errMessage = "", string errTracking = "") => SetErrorInfo(errCode, errMessage, errTracking);

        public Error() { }

        public int ErrorId { get => _errorId; set => _errorId = value; }     
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }  
        public string ErrorTracking { get => _errorTracking; set => _errorTracking = value; }

        /// <summary>
        /// Sets the initial basic error information after (from a try / catch or configured manually)
        /// </summary>
        /// <param name="errCode">The error code to be shown (Customized or acquired from HResult by the C# exception). If OK, returns 0</param>
        /// <param name="errMessage">The error message to be shown (Customized or acquired from Message by the C# exception). If OK, returns ""</param>
        /// <param name="errTracking">The error message details to be shown (Customized or acquired from Stack by the C# exception). If OK, returns ""</param>
        public void SetErrorInfo(int errCode = 0, string errMessage = "", string errTracking = "")
        {
            _errorId = errCode;
            _errorMessage = errMessage;
            _errorTracking = errTracking;
        }

        /// <summary>
        /// Showing a basic error message in console for testing purposes
        /// </summary>
        public virtual void ShowErrorMessageInConsole()
        {
            WriteLine($"Error Code: {_errorId}");
            WriteLine($"Error Description: {_errorMessage}");
            WriteLine($"Error Stack Trace: {_errorTracking}");
        }

    }
}
