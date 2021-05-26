using System;
using SmartAppDataAccess.Interfaces;
using System.Collections.Generic;

namespace SmartAppDataAccess
{
    public class GenericDAO<Item> : IGenericDAO<Item> where Item : class
    {
        private Item _daoItem;          

        public GenericDAO(Item daoItem = default(Item)) 
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
