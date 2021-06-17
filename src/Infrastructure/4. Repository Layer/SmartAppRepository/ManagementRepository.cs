using SmartAppCore.Interfaces.Persistence.Generic;
using SmartAppCore.Interfaces.Persistence;
using SmartAppCore.Interfaces.Repository;
using SmartAppModels.Entities;
using SmartAppRepository.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using GenericErrorHandler;

namespace SmartAppRepository
{
    public class ManagementRepository : MainRepositoryAsync<Mgmt>, IManagementRepository
    {
        private readonly ILogger<SearchRepository> _logger;
        private readonly INoSQLSearchDataAccess _noSQLSearchDataAccess;     //To be used by complex searchs
        private readonly IGenericDAO<Mgmt> _sqlDAODataAccess;               //To be used for CRUD operations

        public ManagementRepository(ILogger<SearchRepository> logger, 
                                    INoSQLSearchDataAccess noSQLSearchDataAccess,
                                    IGenericDAO<Mgmt> sqlDAODataAccess) : base(sqlDAODataAccess){
            _logger = logger;
            _noSQLSearchDataAccess = noSQLSearchDataAccess;
            _sqlDAODataAccess = sqlDAODataAccess;
        }

        //Search Operation
        public Task<Response<Mgmt>> GetResultsFromSearchWithResponse(string searchParams, int limit, string[] markets)
        {
            return null; //Task.Run<Response<Mgmt>>(() => _noSQLSearchDataAccess.GetResultsFromSearchWithResponse(searchParams));
        }

        //CRUD Operations
        public async Task CreateManagement(Mgmt managementItem)
        {
            await _sqlDAODataAccess.CreateItemAsync(managementItem);
        }
    }
}