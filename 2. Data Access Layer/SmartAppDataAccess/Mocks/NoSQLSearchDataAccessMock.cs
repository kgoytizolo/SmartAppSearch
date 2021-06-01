using SmartAppModels;
using SmartAppDataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using GenericErrorHandler;

namespace SmartAppDataAccess.Mocks
{
    public class NoSQLSearchDataAccessMock : INoSQLSearchDataAccess
    {
        private readonly ILogger<NoSQLSearchDataAccessMock> _logger;
        public NoSQLSearchDataAccessMock(ILogger<NoSQLSearchDataAccessMock> logger)
        {
            _logger = logger;
        }

        public SearchedItems GetResultsFromSearch(SearchInputParams searchParams)
        {
            return new SearchedItems { 
                ManagementsFound = new List<Managements>(){ 
                    new Managements(){ Mgmt = { MgmtID = 27918, Name = "Essex Property Trust AKA Essex Apartment Homes", Market = "San Francisco" } },
                    new Managements(){ Mgmt = { MgmtID = 24736, Name = "Privately Owned and Managed", Market = "San Francisco" } } 
                },
                PropertiesFound = new List<SmartAppModels.Properties>(){
                    new SmartAppModels.Properties(){ Property = new Property {PropertyID = 85630, Name = "Curry Junction", FormerName = "", StreetAddress = "3549 Curry Lane", City = "Abilene", Market = "Abilene", State = "TX"}},
                    new SmartAppModels.Properties(){ Property = new Property {PropertyID = 85631, Name = "Riatta Ranch", FormerName = "", StreetAddress = "1111 Musken", City = "Abilene", Market = "Abilene", State = "TX"}}                 
                }
            }; 
        }

        public GenericErrorResponse<SearchedItems> GetResultsFromSearchWithResponse(SearchInputParams searchParams)
        {
            return new GenericErrorResponse<SearchedItems>() { 
                ResponseItem = GetResultsFromSearch(searchParams),
                ErrorId = 0,
                ErrorMessage = "OK"
            };
        }

    }
}