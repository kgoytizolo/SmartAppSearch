using SmartAppModels;
using GenericErrorHandler;

namespace SmartAppDataAccess.Interfaces
{
    public interface INoSQLSearchDataAccess
    {
        GenericErrorResponse<SearchedItems> GetResultsFromSearchWithResponse(SearchInputParams searchParams);
    }
}