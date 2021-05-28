using SmartAppModels;
using System.Threading.Tasks;

namespace SmartAppRepository.Interfaces
{
    public interface ISearchRepository
    {
        Task<SearchedItems> GetResultsFromSearch(SearchInputParams searchParams);
    }
}