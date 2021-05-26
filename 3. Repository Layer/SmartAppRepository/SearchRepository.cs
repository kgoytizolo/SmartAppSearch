using SmartAppRepository.Interfaces;
using SmartAppDataAccess.Interfaces;
using SmartAppModels;
using Microsoft.Extensions.Logging;

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

        public SearchedItems GetResultsFromSearch(SearchInputParams searchParams)
        {
            return _noSQLSearchDataAccess.GetResultsFromSearch(searchParams);
        }

    }
}