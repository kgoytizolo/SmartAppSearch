using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAppCore.Interfaces.Repository.Generic
{
    public interface IMainRepositoryAsync<T> where T : class
    {
        Task<T> GetItemsAsync(int itemId);
        Task<T> GetItemsAsync(string itemId);
        Task CreateItemAsync(T item);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(int itemId);        
    }
}