using SmartAppModels;
using SmartAppDataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Elasticsearch.Net;
using System;

namespace SmartAppDataAccess
{
    public class NoSQLSearchDataAccess : INoSQLSearchDataAccess
    {
        private readonly ILogger<NoSQLSearchDataAccess> _logger;
        private readonly ElasticSearchCnxSettings _cnxSettings;
        private readonly ConnectionConfiguration _cnxConfiguration;
        private ElasticLowLevelClient _elasticCnxLowLevelClient;
        public NoSQLSearchDataAccess(ILogger<NoSQLSearchDataAccess> logger, ElasticSearchCnxSettings cnxSettings)
        {
            _logger = logger;
            _cnxSettings = cnxSettings;
            _cnxConfiguration = new ConnectionConfiguration(new Uri(_cnxSettings.SearchUri)).
                RequestTimeout(TimeSpan.FromMinutes(_cnxSettings.TimeOutInMinutes));
            _elasticCnxLowLevelClient = new ElasticLowLevelClient(_cnxConfiguration);
            _logger.LogInformation("NoSQLSearchDataAccess > Connecting to a new Elastic Search node..");
            _logger.LogInformation($"Search Uri: {_cnxSettings.SearchUri}");
            _logger.LogInformation($"Connection Time Out: {_cnxSettings.TimeOutInMinutes.ToString()}");
        }

        public SearchedItems GetResultsFromSearch(SearchInputParams searchParams)
        {
            return null;
        }
    }
}