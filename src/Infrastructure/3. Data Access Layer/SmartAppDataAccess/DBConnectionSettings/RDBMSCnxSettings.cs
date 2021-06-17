namespace SmartAppDataAccess.DBConnectionSettings
{
    /// <summary>
    /// POCO Class to be used to configure basic RDBMS connection settings (for connection strings)
    /// </summary>
    public class RDBMSCnxSettings : BasicSettings
    {
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string ServerInstance { get; set; }
        public string IntegratedSecurity { get; set; }
        public RDBMSCnxSettings(){}
    }
}