using SmartAppModels;
using SmartAppModels.Entities;
using DBModels = SmartAppDataAccess.Models;
using GenericErrorHandler;
using SmartAppCore.Interfaces.Persistence;
using SmartAppDataAccess.DBConnectionSettings;
using SmartAppDataAccess.Generic;
using SmartAppDataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Elasticsearch.Net;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SmartAppDataAccess
{
    public class NoSQLSearchDataAccess : GenericElasticDataAccess, INoSQLSearchDataAccess
    {
        private INoSQLPropertyDataAccess _noSQLPropertyDataAccess;
        private INoSQLManagementDataAccess _noSQLManagementDataAccess;    

        public NoSQLSearchDataAccess(ILogger<NoSQLSearchDataAccess> logger, 
                                     ElasticSearchCnxSettings cnxSettings) : base (logger, cnxSettings) {
            _noSQLPropertyDataAccess = new NoSQLPropertyDataAccess(_elasticCnxNESTClient);
            _noSQLManagementDataAccess = new NoSQLManagementDataAccess(_elasticCnxNESTClient);
        }

        public Response<SearchedItems> GetResultsFromSearchWithResponse(string SearchPhrase, int limit, string[] markets)
        {
            Exception responseException = new Exception();
            var response = new Response<SearchedItems>();
            var searchedResults = (_cnxSettings.UseNESTClient) ? GetResultsFromSearchNESTClient(SearchPhrase, limit, markets, ref responseException)  
                                                               : GetResultsFromSearchLowLevelClient(SearchPhrase, limit, markets, ref responseException);
            if(searchedResults == null) 
                response.SetErrorInfo(9404, $"Requested search was not found for full text query.");
            else{
                if(responseException == null)  response.ResponseItem = searchedResults;
                else {
                    string errGenericMessage = "Error trying to obtain a search response from Elasticsearch property index.";
                    LogErrorMessages(errGenericMessage, responseException.HResult, responseException.Message ?? "", "", "");
                    response.SetErrorInfo(9000, $"Internal Server error:. Exception error: {errGenericMessage} ", "");
                }
            }          
            return response;
        }

        private SearchedItems GetResultsFromSearchLowLevelClient(string searchPhrase, int limit, string[] markets, ref Exception exception)
        {
            List<Property> searchedProperties = new();
            List<Mgmt> searchedManagements = new();
            SearchedItems search = new();

            var searchResponse = _elasticCnxLowLevelClient.Search<StringResponse>(
                "properties", _noSQLPropertyDataAccess.GetSerializablePostDataForProperties(searchPhrase, limit, markets));
            if(searchResponse.Success){
                search.PropertiesFound = searchedProperties;
                search.ManagementsFound = searchedManagements;
                exception = null;
            }
            else exception = searchResponse.OriginalException;

            var responseJson = searchResponse.Body;
            return null;
        }

        private SearchedItems GetResultsFromSearchNESTClient(string searchPhrase, int limit, string[] markets, ref Exception exception)
        {
            List<DBModels.Properties> searchedProperties = new();
            List<DBModels.Managements> searchedManagements = new();
            SearchedItems search = new();

            var searchResponseProperties = _noSQLPropertyDataAccess.GetSerializableSearchResponseForProperty(searchPhrase, limit, markets);
            var searchResponseManagements = _noSQLManagementDataAccess.GetSerializableSearchResponseForManagement(searchPhrase, limit, markets);

            if(searchResponseProperties.OriginalException == null){
                var properties = searchResponseProperties.Documents;
                searchedProperties = (properties == null) ? null : properties.ToList<DBModels.Properties>();
                searchedProperties.ForEach(x => search.PropertiesFound.Add(x.Property));
                exception = null;
            }
            else exception = searchResponseProperties.OriginalException;

            if(searchResponseManagements.OriginalException == null){
                var managements = searchResponseManagements.Documents;
                searchedManagements = (managements == null) ? null : managements.ToList<DBModels.Managements>();
                searchedManagements.ForEach(y => search.ManagementsFound.Add(y.Mgmt));
                exception = null;
            }
            else exception = searchResponseProperties.OriginalException;            

            return search;
        }

    }
}