using SmartAppModels;
using GenericErrorHandler;
using SmartAppDataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Elasticsearch.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nest;
using System;
using System.Linq;

namespace SmartAppDataAccess
{
    public class NoSQLSearchDataAccess : INoSQLSearchDataAccess
    {
        private readonly ILogger<NoSQLSearchDataAccess> _logger;
        private readonly ElasticSearchCnxSettings _cnxSettings;
        private readonly ConnectionConfiguration _cnxConfigurationLowLevelClient;
        private readonly ConnectionSettings _cnxSettingsNESTClient;
        private ElasticLowLevelClient _elasticCnxLowLevelClient;
        private ElasticClient _elasticCnxNESTClient;
        private bool _isCnxAvailable;
        public NoSQLSearchDataAccess(ILogger<NoSQLSearchDataAccess> logger, ElasticSearchCnxSettings cnxSettings)
        {
            _logger = logger;
            _cnxSettings = cnxSettings;

            try{
                if(_cnxSettings.UseNESTClient){
                    _cnxSettingsNESTClient = new ConnectionSettings(new System.Uri(_cnxSettings.SearchUri)).
                        PrettyJson().
                        DisableDirectStreaming().
                        RequestTimeout(System.TimeSpan.FromMinutes(_cnxSettings.TimeOutInMinutes)).
                        OnRequestCompleted(details => _logger.LogInformation(System.Text.Encoding.UTF8.GetString(details.RequestBodyInBytes)));;
                    _elasticCnxNESTClient = new ElasticClient(_cnxSettingsNESTClient);
                }
                else{
                    _cnxConfigurationLowLevelClient = new ConnectionConfiguration(new System.Uri(_cnxSettings.SearchUri)).
                        PrettyJson().
                        RequestTimeout(System.TimeSpan.FromMinutes(_cnxSettings.TimeOutInMinutes));
                    _elasticCnxLowLevelClient = new ElasticLowLevelClient(_cnxConfigurationLowLevelClient);
                }
                _logger.LogInformation("NoSQLSearchDataAccess > Connecting to a new Elastic Search node..");
                _logger.LogInformation($"Search Uri: {_cnxSettings.SearchUri}");
                _logger.LogInformation($"Connection Time Out (In minutes): {_cnxSettings.TimeOutInMinutes.ToString()}");
                _isCnxAvailable = true;
            }
            catch(System.Exception e)
            {
                LogErrorMessages("Error trying to connect to the Elasticsearch cluster/node resource.", 
                    e.HResult, e.Message, e.StackTrace, e.InnerException.GetType().Name);            
            }
        }

        public GenericErrorResponse<SearchedItems> GetResultsFromSearchWithResponse(SearchInputParams searchParams)
        {
            Exception responseException = new Exception();
            var response = new GenericErrorResponse<SearchedItems>();
            if(_isCnxAvailable){
                var searchedResults = (_cnxSettings.UseNESTClient) ? GetResultsFromSearchNESTClient(searchParams, ref responseException)  
                                                                   : GetResultsFromSearchLowLevelClient(searchParams, ref responseException);
                if(searchedResults == null) 
                    response.SetErrorInfo(9404, $"Requested search was not found for full text query.");
                else{
                    if(responseException == null)  response.ResponseItem = searchedResults;
                    else {
                        string errGenericMessage = "Error trying to obtain a search response from Elasticsearch property index.";
                        LogErrorMessages(errGenericMessage, 
                            responseException.HResult, responseException.Message ?? "", "", "");
                        response.SetErrorInfo(9000, $"Internal Server error:. Exception error: {errGenericMessage} ", "");
                    }                    
                }                   
            }
            else {
                _logger.LogError("Elastic Search connection is not available.");
                response.SetErrorInfo(9999, "Elastic Search connection is not available.");
            }
            return response;
        }

        private SearchedItems GetResultsFromSearchLowLevelClient(SearchInputParams searchParams, ref Exception exception)
        {
            List<SmartAppModels.Properties> searchedProperties = new();
            List<SmartAppModels.Managements> searchedManagements = new();
            SearchedItems search = new();

            var searchResponse = _elasticCnxLowLevelClient.Search<StringResponse>(
                "properties", GetSerializablePostDataForProperties(searchParams));
            if(searchResponse.Success){
                search.PropertiesFound = searchedProperties;
                search.ManagementsFound = searchedManagements;
                exception = null;
            }
            else exception = searchResponse.OriginalException;

            var responseJson = searchResponse.Body;
            return null;
        }

        private SearchedItems GetResultsFromSearchNESTClient(SearchInputParams searchParams, ref Exception exception)
        {
            List<SmartAppModels.Properties> searchedProperties = new();
            List<SmartAppModels.Managements> searchedManagements = new();
            SearchedItems search = new();

            var searchResponseProperties = GetSerializableSearchResponseForProperty(searchParams);
            var searchResponseManagements = GetSerializableSearchResponseForManagement(searchParams);

            if(searchResponseProperties.OriginalException == null){
                var properties = searchResponseProperties.Documents;
                searchedProperties = (properties == null) ? null : properties.ToList<SmartAppModels.Properties>();
                search.PropertiesFound = searchedProperties;
                exception = null;
            }
            else exception = searchResponseProperties.OriginalException;

            if(searchResponseManagements.OriginalException == null){
                var managements = searchResponseManagements.Documents;
                searchedManagements = (managements == null) ? null : managements.ToList<SmartAppModels.Managements>();                
                search.ManagementsFound = searchedManagements;
                exception = null;
            }
            else exception = searchResponseProperties.OriginalException;            

            return search;
        }

        private PostData GetSerializablePostDataForProperties(SearchInputParams searchParams){
            PostData postPropertiesIndex = PostData.Serializable(new
            {
                from = 0,
                size = searchParams.Limit,
                query = new {
                    match = new { 
                        property = new {
                            name = new{
                                query = searchParams.SearchPhase
                            }
                        }
                    }
                }
            }
            );
            return postPropertiesIndex;
        }

        private ISearchResponse<SmartAppModels.Properties> GetSerializableSearchResponseForProperty(SearchInputParams searchParams){
            var searchResponse =  _elasticCnxNESTClient.Search<SmartAppModels.Properties>(s => s
                    .Index("property")
                    .TypedKeys(null)
                    .From(0)
                    .Size(searchParams.Limit)
                    .Query(q => q
                        .Match(m => m
                            .Field(f => f.Property.Name)
                            .Query(searchParams.SearchPhase)
                        )
                    )
                );

            return searchResponse;
        }

        private ISearchResponse<Managements> GetSerializableSearchResponseForManagement(SearchInputParams searchParams){
            var searchResponse = _elasticCnxNESTClient.Search<Managements>(s => s
                    .Index("management")
                    .TypedKeys(null)
                    .From(0)
                    .Size(searchParams.Limit)
                    .Query(q => q
                        .Match(m => m
                            .Field(f => f.Mgmt.Name)
                            .Query(searchParams.SearchPhase)
                        )
                    )
                );
            return searchResponse;
        }

        private void LogErrorMessages(string logError, int errorId, string errorMsg, string errorTracking, string innerException)
        {
                _logger.LogError($"{logError}");
                _logger.LogError($"Error code: {errorId}");
                _logger.LogError($"Error message: {errorMsg}");
                _logger.LogError($"Error type: {innerException}");
                _logger.LogError($"Error trace: {errorTracking}");                
        }

    }
}