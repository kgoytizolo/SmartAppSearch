using SmartAppModels;
using GenericErrorHandler;

namespace SmartAppDataAccess.Interfaces
{
    public interface INoSQLSearchDataAccess
    {
        SearchedItems GetResultsFromSearch(SearchInputParams searchParams);
        GenericErrorResponse<SearchedItems> GetResultsFromSearchWithResponse(SearchInputParams searchParams);
    }
}