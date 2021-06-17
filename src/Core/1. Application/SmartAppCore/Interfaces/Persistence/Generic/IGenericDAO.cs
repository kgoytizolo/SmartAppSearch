namespace SmartAppCore.Interfaces.Persistence.Generic
{
    public interface IGenericDAO<Item> : IGenericDataAccess<Item> where Item : class { }
}