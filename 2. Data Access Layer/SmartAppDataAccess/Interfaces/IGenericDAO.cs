using System;
using System.Collections.Generic;

namespace SmartAppDataAccess.Interfaces
{
    public interface IGenericDAO<Item> : IGenericDataAccess<Item> where Item : class { }
}