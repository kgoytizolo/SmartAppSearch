using System;
using SmartAppCore.Interfaces.Persistence.Generic;
using System.Threading.Tasks;

namespace SmartAppDataAccess.Generic
{
    /// <summary>
    /// A Generic Class (Unit of Work - UOW) used for CRUD operations, based on DAO (Data Access Objects)
    /// </summary>
    /// <typeparam name="Item">Generic class which will be the base of CRUD Operations</typeparam>
    public class GenericDAO<Item> : IGenericDAO<Item> where Item : class
    {
        private Item _daoItem;          

        public GenericDAO(Item daoItem = default(Item)) 
        {
            _daoItem = daoItem;
        }

        public Task CreateItemAsync(Item item)
        {
            throw new NotImplementedException();    //TODO: To implement DAO code 
        }

        public Task DeleteItemAsync(int itemId)
        {
            throw new NotImplementedException();    //TODO: To implement DAO code 
        }

        public Task<Item> GetItemsAsync(int itemId)
        {
            throw new NotImplementedException();    //TODO: To implement DAO code 
        }

        public Task<Item> GetItemsAsync(string itemId)
        {
            throw new NotImplementedException();    //TODO: To implement DAO code 
        }

        public Task UpdateItemAsync(Item item)
        {
            throw new NotImplementedException();    //TODO: To implement DAO code 
        }
    }
}
