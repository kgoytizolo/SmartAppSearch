using System;
using System.Threading.Tasks;
using SmartAppCore.Interfaces.Persistence.Generic;

namespace SmartAppDataAccess.Mocks
{
    public class GenericDAOMock<Item> : IGenericDataAccess<Item> where Item : class
    {
        public Task CreateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemsAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemsAsync(string itemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }
    }
}