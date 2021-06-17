using SmartAppModels;
using GenericErrorHandler;

namespace SmartAppCore.Interfaces.Persistence
{
    public interface INoSQLSearchDataAccess
    {
        Response<SearchedItems> GetResultsFromSearchWithResponse(string searchPhrase, int limit, string[] markets);
    }
}