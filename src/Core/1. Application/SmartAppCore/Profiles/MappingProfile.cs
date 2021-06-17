using AutoMapper;
using SmartAppCore.Features.Searchs.GetSearchResults;
using SmartAppModels.Entities;
using SmartAppModels;
using System.Collections.Generic;

namespace SmartAppCore.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyDto>();
            CreateMap<Mgmt, ManagementDto>();
            CreateMap<List<Property>, List<PropertyDto>>();
            CreateMap<List<Mgmt>, List<ManagementDto>>();            
            CreateMap<SearchedItems, SearchedItemsViewModels>(); 
        }
    }
}