using SmartAppModels;

namespace SmartAppRepository.Interfaces
{
    public interface ISearchRepository
    {
        SearchedItems GetResultsFromSearch(SearchInputParams searchParams);
    }
}