using SmartAppModels;
using DBModels = SmartAppDataAccess.Models;
using Nest;
using Elasticsearch.Net;

namespace SmartAppDataAccess.Interfaces
{
    public interface INoSQLPropertyDataAccess
    {
        ISearchResponse<DBModels.Properties> GetSerializableSearchResponseForProperty(string searchPhrase, int limit, string[] markets);
        PostData GetSerializablePostDataForProperties(string searchPhrase, int limit, string[] markets);
    }
}