using SmartAppRepository.Interfaces;
using SmartAppDataAccess.Interfaces;
using SmartAppModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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

        public Task<SearchedItems> GetResultsFromSearch(SearchInputParams searchParams)
        {
            return Task.Run<SearchedItems>(() => _noSQLSearchDataAccess.GetResultsFromSearch(searchParams));
        }

    }
}