using SmartAppModels;
using GenericErrorHandler;
using SmartAppDataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Elasticsearch.Net;
using System.Collections.Generic;

namespace SmartAppDataAccess
{
    public class NoSQLSearchDataAccess : INoSQLSearchDataAccess
    {
        private readonly ILogger<NoSQLSearchDataAccess> _logger;
        private readonly ElasticSearchCnxSettings _cnxSettings;
        private readonly ConnectionConfiguration _cnxConfiguration;
        private ElasticLowLevelClient _elasticCnxLowLevelClient;
        private bool _isCnxAvailable;
        public NoSQLSearchDataAccess(ILogger<NoSQLSearchDataAccess> logger, ElasticSearchCnxSettings cnxSettings)
        {
            _logger = logger;
            _cnxSettings = cnxSettings;

            try{
                _cnxConfiguration = new ConnectionConfiguration(new System.Uri(_cnxSettings.SearchUri)).
                    RequestTimeout(System.TimeSpan.FromMinutes(_cnxSettings.TimeOutInMinutes));
                _elasticCnxLowLevelClient = new ElasticLowLevelClient(_cnxConfiguration);
                _logger.LogInformation("NoSQLSearchDataAccess > Connecting to a new Elastic Search node..");
                _logger.LogInformation($"Search Uri: {_cnxSettings.SearchUri}");
                _logger.LogInformation($"Connection Time Out (In minutes): {_cnxSettings.TimeOutInMinutes.ToString()}");
                _isCnxAvailable = true;
            }
            catch(System.Exception e)
            {
                _logger.LogError($"Error trying to connect to the Elasticsearch cluster/node resource.");
                _logger.LogError($"Error code: {e.HResult}");
                _logger.LogError($"Error message: {e.Message}");
                _logger.LogError($"Error type: {e.InnerException.GetType().Name}");
                _logger.LogError($"Error trace: {e.StackTrace}");                
            }
        }

        public SearchedItems GetResultsFromSearch(SearchInputParams searchParams)
        {
            var response = GetResultsFromSearchWithResponse(searchParams);
            return response.ResponseItem;
        }

        public GenericErrorResponse<SearchedItems> GetResultsFromSearchWithResponse(SearchInputParams searchParams)
        {
            if(_isCnxAvailable){
                var response = new GenericErrorResponse<SearchedItems>();
                var searchedResults = GetResultsFromSearchMock();
                if(searchedResults == null) 
                    response.SetErrorInfo(9999, $"Requested search was not found for full text query.");
                else 
                    response.ResponseItem = searchedResults;
                return response;
            }
            else {                
                return null;        
            }
        }

        //******************* Temporary results ******************
        private SearchedItems GetResultsFromSearchMock()
        {
            return new SearchedItems { 
                ManagementsFound = new List<Management>(){ 
                    new Management(){ MgmtID = 27918, Name = "Essex Property Trust AKA Essex Apartment Homes", Market = "San Francisco" },
                    new Management(){ MgmtID = 24736, Name = "Privately Owned and Managed", Market = "San Francisco" }  
                },
                PropertiesFound = new List<Property>(){
                    new Property(){ PropertyID = 85630, Name = "Curry Junction", FormerName = "", StreetAddress = "3549 Curry Lane", City = "Abilene", Market = "Abilene", State = "TX"},
                    new Property(){ PropertyID = 85631, Name = "Riatta Ranch", FormerName = "", StreetAddress = "1111 Musken", City = "Abilene", Market = "Abilene", State = "TX"}                
                }
            }; 
        }


    }
}