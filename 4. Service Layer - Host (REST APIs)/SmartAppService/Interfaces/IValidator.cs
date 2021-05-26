namespace SmartAppService.Interfaces
{
    //General method for every param filter to be vaildated as a standard
    public interface IValidator
    {
        (bool isValidationOk, string validationMessage) Validate<T>(T input = default(T)) where T : class;
    }
}