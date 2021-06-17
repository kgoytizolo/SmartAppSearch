namespace SmartAppDataAccess.DBConnectionSettings
{
    /// <summary>
    /// POCO Class to be used to configure cloud-based connection settings (RDBMS and NOSQL DBS)
    /// </summary>
    public class CloudCnxSettings : BasicSettings
    {
        public string SearchUri { get; set; }
        public string ClusterId { get; set; }
        public string Region { get; set; }
        public string CloudPlatform  { get; set; }
        public string CloudDomain  { get; set; }
        public short Port  { get; set; }
        public CloudCnxSettings(){}
    }
}