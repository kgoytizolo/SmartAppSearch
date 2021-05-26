using SmartAppModels;
using SmartAppDataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace SmartAppDataAccess
{
    public class NoSQLSearchDataAccess : INoSQLSearchDataAccess
    {
        private readonly ILogger<NoSQLSearchDataAccess> _logger;        
        public NoSQLSearchDataAccess(ILogger<NoSQLSearchDataAccess> logger)
        {
            _logger = logger;
        }

        public SearchedItems GetResultsFromSearch(SearchInputParams searchParams)
        {
            return null;
        }
    }
}