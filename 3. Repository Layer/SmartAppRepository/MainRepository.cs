using System;
using SmartAppRepository.Interfaces;
using System.Collections.Generic;
using SmartAppDataAccess.Interfaces;

namespace SmartAppRepository
{
    public class MainRepository<T> : IMainRepository<T> where T : class
    {
        private T _repositoryItem;
        private IGenericDataAccess<T> _smartAppDataAccess;

        public MainRepository(IGenericDataAccess<T> dataAccess, T repositoryItem = default(T))
        {
            _repositoryItem = repositoryItem;
            _smartAppDataAccess = dataAccess;
        }

        public void CreateItem(T item)
        {
            _smartAppDataAccess.CreateItem(item);
        }

        public void DeleteItem(int itemId)
        {
            _smartAppDataAccess.DeleteItem(itemId);
        }

        public void GetItems(int itemId, ref IEnumerable<T> listOfItems)
        {
            _smartAppDataAccess.GetItems(itemId, ref listOfItems);
        }

        public void GetItems(string itemId, ref IEnumerable<T> listOfItems)
        {
            _smartAppDataAccess.GetItems(itemId, ref listOfItems);
        } 

        public void UpdateItem(T item)
        {
            _smartAppDataAccess.UpdateItem(item);
        }

    }
}
