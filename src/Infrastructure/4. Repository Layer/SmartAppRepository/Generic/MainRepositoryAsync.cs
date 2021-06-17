using SmartAppCore.Interfaces.Repository.Generic;
using SmartAppCore.Interfaces.Persistence.Generic;
using System.Threading.Tasks;

namespace SmartAppRepository.Generic
{
    public class MainRepositoryAsync<T> : IMainRepositoryAsync<T> where T : class
    {
        private IGenericDataAccess<T> _smartAppDataAccess;

        public MainRepositoryAsync(IGenericDataAccess<T> dataAccess)
        {
            _smartAppDataAccess = dataAccess;
        }

        public async Task CreateItemAsync(T item)
        {
            await _smartAppDataAccess.CreateItemAsync(item);
        }

        public async Task DeleteItemAsync(int itemId)
        {
            await _smartAppDataAccess.DeleteItemAsync(itemId);
        }

        public async Task<T> GetItemsAsync(int itemId)
        {
            return await _smartAppDataAccess.GetItemsAsync(itemId);
        }

        public async Task<T> GetItemsAsync(string itemId)
        {
            return await _smartAppDataAccess.GetItemsAsync(itemId);
        }

        public async Task UpdateItemAsync(T item)
        {
            await _smartAppDataAccess.UpdateItemAsync(item);
        }
    }
}
