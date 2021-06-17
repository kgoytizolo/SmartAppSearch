using SmartAppDataAccess.DBConnectionSettings;
using Microsoft.Extensions.Logging;
using Elasticsearch.Net;
using Nest;

namespace SmartAppDataAccess.Generic
{
    public class GenericElasticDataAccess
    {
        protected readonly ILogger<GenericElasticDataAccess> _logger;
        protected readonly ElasticSearchCnxSettings _cnxSettings;
        protected readonly ConnectionConfiguration _cnxConfigurationLowLevelClient;
        protected readonly ConnectionSettings _cnxSettingsNESTClient;
        protected ElasticLowLevelClient _elasticCnxLowLevelClient;
        protected ElasticClient _elasticCnxNESTClient;   

        public GenericElasticDataAccess(ILogger<GenericElasticDataAccess> logger, ElasticSearchCnxSettings cnxSettings)
        {
            _logger = logger;
            _cnxSettings = cnxSettings;

            try{
                if(_cnxSettings.UseNESTClient){
                    _cnxSettingsNESTClient = ConnectViaNESTClient();
                    _elasticCnxNESTClient = new ElasticClient(_cnxSettingsNESTClient);
                }
                else{
                    _cnxConfigurationLowLevelClient = ConnectViaLowLevelClient();
                    _elasticCnxLowLevelClient = new ElasticLowLevelClient(_cnxConfigurationLowLevelClient);
                }
                LogConnectivityInformation();
            }
            catch(System.Exception e)
            {
                LogErrorMessages("Error trying to connect to the Elasticsearch cluster/node resource.", 
                    e.HResult, e.Message, e.StackTrace, e.InnerException.GetType().Name);            
            }
        }

        //********************* Private methods **********************************
        protected ConnectionSettings ConnectViaNESTClient()
        {
            return new ConnectionSettings(new System.Uri(_cnxSettings.SearchUri)).
                        PrettyJson().
                        DisableDirectStreaming().
                        RequestTimeout(System.TimeSpan.FromMinutes(_cnxSettings.TimeOutInMinutes)).
                        OnRequestCompleted(details => _logger.LogInformation(
                            System.Text.Encoding.UTF8.GetString(details.RequestBodyInBytes)));;
        }

        protected ConnectionConfiguration ConnectViaLowLevelClient(){
            return new ConnectionConfiguration(new System.Uri(_cnxSettings.SearchUri)).
                        PrettyJson().
                        RequestTimeout(System.TimeSpan.FromMinutes(_cnxSettings.TimeOutInMinutes));
        }

        protected void LogErrorMessages(string logError, int errorId, string errorMsg, string errorTracking, string innerException)
        {
            _logger.LogError($"{logError}");
            _logger.LogError($"Error code: {errorId}");
            _logger.LogError($"Error message: {errorMsg}");
            _logger.LogError($"Error type: {innerException}");
            _logger.LogError($"Error trace: {errorTracking}");                
        }

        protected void LogConnectivityInformation(){
            _logger.LogInformation("NoSQLSearchDataAccess > Connecting to a new Elastic Search node..");
            _logger.LogInformation($"Search Uri: {_cnxSettings.SearchUri}");
            _logger.LogInformation($"Connection Time Out (In minutes): {_cnxSettings.TimeOutInMinutes.ToString()}");
        }

    }
}