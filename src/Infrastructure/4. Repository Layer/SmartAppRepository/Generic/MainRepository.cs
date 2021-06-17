using SmartAppCore.Interfaces.Repository.Generic;
using System.Collections.Generic;
using SmartAppCore.Interfaces.Persistence.Generic;

namespace SmartAppRepository.Generic
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
            throw new System.NotImplementedException();
        }

        public void DeleteItem(int itemId)
        {
            throw new System.NotImplementedException();
        }

        public void GetItems(int itemId, ref IEnumerable<T> listOfItems)
        {
            throw new System.NotImplementedException();
        }

        public void GetItems(string itemId, ref IEnumerable<T> listOfItems)
        {
            throw new System.NotImplementedException();
        } 

        public void UpdateItem(T item)
        {
            throw new System.NotImplementedException();
        }

    }
}
