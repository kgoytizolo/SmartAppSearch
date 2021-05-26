using SmartAppModels;

namespace SmartAppDataAccess.Interfaces
{
    public interface INoSQLSearchDataAccess
    {
        SearchedItems GetResultsFromSearch(SearchInputParams searchParams);
    }
}