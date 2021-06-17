using SmartAppService.Interfaces;
using System.Reflection;
using SmartAppService.Models;

namespace SmartAppService.Validations
{
    /// <summary>
    /// This class allows the service to send customized parameters to be sent to AWS Elasticsearch
    /// </summary>
    public class SearchValidator : ISearchValidator
    {
        private readonly int _absoluteLimit = 10;   //The minimum result limit
        private readonly int _defaultLimit = 25;    //The default result to be returned if filter is not retrieved

        public (bool isValidationOk, string validationMessage) Validate<T>(T input) where T : class{
            SearchInputParams inputSearch = input as SearchInputParams;
            PropertyInfo[] propertyClassList = inputSearch.GetType().GetProperties();
            (bool isOk, string validMsg) validation = new();
            foreach(var propertyClass in propertyClassList){
                validation = propertyClass.Name switch {
                    "SearchPhase"   => ValidateSearchPhase(inputSearch.SearchPhase),
                    "Limit"         => ValidateLimit(inputSearch.Limit, ref inputSearch),
                    "Markets"       => (true, "OK (list of markets are optional)."),
                    _               => (false, "Field not recognized")
                };
                if (!validation.isOk)  break; 
            }
            return validation;
        }

        //******************** Validation Private methods ************************************

        private (bool, string) ValidateSearchPhase(string evaluatedValue)
        {
            if(string.IsNullOrEmpty(evaluatedValue) || string.IsNullOrWhiteSpace(evaluatedValue))  
                return (false, "EvaluatedValue is null and this is a mandatory field.");            
            return (true, $"OK - Search Phase Value: {evaluatedValue}");
        }

        private (bool, string) ValidateLimit(int? evaluatedValue, ref SearchInputParams inputSearch){
            string message = "OK - Limit Value";
            if(evaluatedValue == null || evaluatedValue == default(int) || evaluatedValue < _absoluteLimit){
                inputSearch.Limit = _defaultLimit;
                message += " (default)";
            }
            return (true, $"{message}: {evaluatedValue}");
        }

    }
}