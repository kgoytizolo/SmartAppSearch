using SmartAppCore.Interfaces.Repository;
using SmartAppCore.Interfaces.Persistence;
using SmartAppModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using GenericErrorHandler;

namespace SmartAppRepository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ILogger<SearchRepository> _logger;
        private readonly INoSQLSearchDataAccess _noSQLSearchDataAccess;

        public SearchRepository(ILogger<SearchRepository> logger, INoSQLSearchDataAccess noSQLSearchDataAccess){
            _logger = logger;
            _noSQLSearchDataAccess = noSQLSearchDataAccess;
        }

        public Task<Response<SearchedItems>> GetResultsFromSearchWithResponse(string SearchPhrase, int limit, string[] markets)
        {
            return Task.Run<Response<SearchedItems>>(() => _noSQLSearchDataAccess.GetResultsFromSearchWithResponse(SearchPhrase, limit, markets));
        }

        //string SearchPhrase, int limit, List<string> markets

    }
}