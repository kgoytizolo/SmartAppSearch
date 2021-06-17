using System.Collections.Generic;

namespace SmartAppCore.Features.Searchs.GetSearchResults
{
    public class SearchedItemsViewModels
    {
        public List<ManagementDto> ManagementsFound { get; set; }
        public List<PropertyDto> PropertiesFound { get; set; }    
    }
}