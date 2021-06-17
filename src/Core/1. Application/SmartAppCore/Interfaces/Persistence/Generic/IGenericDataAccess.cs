using System.Threading.Tasks;

namespace SmartAppCore.Interfaces.Persistence.Generic
{
    /// <summary>
    /// This class can be used for CRUD methods applied to any type of data connection
    /// To be reused in: DAO Connections, Entity Framework, etc for SQL and NOSQL databases
    /// </summary>
    /// <typeparam name="Item">The entity or class to be processed in a data access layer</typeparam>
    public interface IGenericDataAccess<Item> where Item : class        
    {
        Task<Item> GetItemsAsync(int itemId);         //In case of integer IDs
        Task<Item> GetItemsAsync(string itemId);      //In case of string (codes) or GUID IDs        
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int itemId);        
    }
}
