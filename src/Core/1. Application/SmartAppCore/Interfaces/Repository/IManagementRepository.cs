using System.Threading.Tasks;
using SmartAppModels.Entities;
using SmartAppCore.Interfaces.Repository.Generic;
using SmartAppModels;
using GenericErrorHandler;

namespace SmartAppCore.Interfaces.Repository
{
    public interface IManagementRepository
    { 
        Task<Response<Mgmt>> GetResultsFromSearchWithResponse(string searchPhrase, int limit, string[] markets);               
    }
}