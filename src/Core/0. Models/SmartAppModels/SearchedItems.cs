using System.Collections.Generic;
using SmartAppModels.Entities;

namespace SmartAppModels
{
    public class SearchedItems
    {
        public List<Mgmt> ManagementsFound { get; set; }
        public List<Property> PropertiesFound { get; set; }
    }
}