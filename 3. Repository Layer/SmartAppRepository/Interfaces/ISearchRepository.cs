using SmartAppModels;
using System.Threading.Tasks;
using GenericErrorHandler;

namespace SmartAppRepository.Interfaces
{
    public interface ISearchRepository
    {
        Task<SearchedItems> GetResultsFromSearch(SearchInputParams searchParams);
        Task<GenericErrorResponse<SearchedItems>> GetResultsFromSearchWithResponse(SearchInputParams searchParams);       
    }
}