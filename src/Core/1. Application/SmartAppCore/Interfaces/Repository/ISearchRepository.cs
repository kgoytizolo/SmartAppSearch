using SmartAppModels;
using System.Threading.Tasks;
using GenericErrorHandler;

namespace SmartAppCore.Interfaces.Repository
{
    public interface ISearchRepository
    {
        Task<Response<SearchedItems>> GetResultsFromSearchWithResponse(string searchPhrase, int limit, string[] markets);  
    }
}