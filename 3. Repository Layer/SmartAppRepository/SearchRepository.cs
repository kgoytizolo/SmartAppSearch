using SmartAppRepository.Interfaces;
using SmartAppDataAccess.Interfaces;
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

        public Task<GenericErrorResponse<SearchedItems>> GetResultsFromSearchWithResponse(SearchInputParams searchParams)
        {
            return Task.Run<GenericErrorResponse<SearchedItems>>(() => _noSQLSearchDataAccess.GetResultsFromSearchWithResponse(searchParams));
        }

    }
}