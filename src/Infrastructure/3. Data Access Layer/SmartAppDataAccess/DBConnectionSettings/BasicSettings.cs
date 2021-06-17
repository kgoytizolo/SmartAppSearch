namespace SmartAppDataAccess.DBConnectionSettings
{
    /// <summary>
    /// POCO Class to be used to configure basic classic user - password and timeout connection settings
    /// </summary>
    public class BasicSettings
    {
        public string UserCnx { get; set; }
        public string PasswordCnx { get; set; }
        public byte TimeOutInMinutes { get; set; }
        public BasicSettings(){}
    }
}