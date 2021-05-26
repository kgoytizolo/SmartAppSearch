using System;
using System.Collections.Generic;
using SmartAppDataAccess.Interfaces;

namespace SmartAppDataAccess.Mocks
{
    public class GenericDAOMock<Item> : IGenericDataAccess<Item> where Item : class
    {
        private Item _daoItem;          

        public GenericDAOMock(Item daoItem = default(Item)) 
        {
            _daoItem = daoItem;
        }

        public void CreateItem(Item item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public void GetItems(int itemId, ref IEnumerable<Item> input)
        {
            throw new NotImplementedException();
        }

        public void GetItems(string itemId, ref IEnumerable<Item> input)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }        
    }
}