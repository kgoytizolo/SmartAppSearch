namespace SmartAppDataAccess.DBConnectionSettings
{
    /// <summary>
    /// POCO Class to be used to configure Elastic Search Cnx Settings
    /// </summary>
    public class ElasticSearchCnxSettings : CloudCnxSettings
    {
        public bool UriOnly  { get; set; }
        public string AccessKey { get; set; }
        public bool UseNESTClient { get; set; }

        public ElasticSearchCnxSettings(){} 
    }
}