using SmartAppModels;
using DBModels = SmartAppDataAccess.Models;
using Nest;

namespace SmartAppDataAccess.Interfaces
{
    public interface INoSQLManagementDataAccess
    {
        ISearchResponse<DBModels.Managements> GetSerializableSearchResponseForManagement(string searchPhrase, int limit, string[] markets);
    }
}