using System;
using System.Collections.Generic;

namespace SmartAppDataAccess.Interfaces
{
    /// <summary>
    /// This class can be used for CRUD methods applied to any type of data connection
    /// To be reused in: DAO Connections, Entity Framework, etc for SQL and NOSQL databases
    /// </summary>
    /// <typeparam name="Item">The entity or class to be processed in a data access layer</typeparam>
    public interface IGenericDataAccess<Item> where Item : class        
    {
        void GetItems(int itemId, ref IEnumerable<Item> input);         //In case of integer IDs
        void GetItems(string itemId, ref IEnumerable<Item> input);      //In case of string (codes) or GUID IDs        
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(int itemId);        
    }
}
