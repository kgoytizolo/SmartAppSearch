namespace SmartAppDataAccess
{
    /// <summary>
    /// POCO Class to be used to configure Elastic Search Cnx Settings
    /// </summary>
    public class ElasticSearchCnxSettings
    {
        public string SearchUri { get; set; }
        public string ClusterId { get; set; }
        public string Region { get; set; }
        public string CloudPlatform  { get; set; }
        public string CloudDomain  { get; set; }
        public short Port  { get; set; }
        public bool UriOnly  { get; set; }
        public string UserCnx { get; set; }
        public string PasswordCnx { get; set; }
        public string AccessKey { get; set; }
        public byte TimeOutInMinutes { get; set; }
    }
}