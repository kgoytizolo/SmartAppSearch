namespace SmartAppDataAccess.Interfaces
{
    public interface IElasticSearchCnxSettings
    {
        string SearchUri { get ; }
        string ClusterId { get ; }
        string Region { get ; }
        string CloudPlatform  { get ; }
        string CloudDomain  { get ; }
        short Port  { get ; }
        bool UriOnly  { get ; }
    }
}