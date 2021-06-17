using SmartAppModels;
using SmartAppModels.Entities;
using SmartAppCore.Interfaces.Persistence;
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

        public Response<SearchedItems> GetResultsFromSearchWithResponse(string searchPhrase, int limit, string[] markets)
        {
            return new Response<SearchedItems>() { 
                ResponseItem = GetResultsFromSearch(searchPhrase, limit, markets),
                ErrorId = 0,
                ErrorMessage = "OK"
            };
        }

        private SearchedItems GetResultsFromSearch(string searchPhrase, int limit, string[] markets)
        {
            return new SearchedItems { 
                ManagementsFound = new List<Mgmt>(){ 
                    new Mgmt() { MgmtID = 27918, Name = "Essex Property Trust AKA Essex Apartment Homes", Market = "San Francisco" },
                    new Mgmt() { MgmtID = 24736, Name = "Privately Owned and Managed", Market = "San Francisco" }  
                },
                PropertiesFound = new List<Property>(){
                    new Property() {PropertyID = 85630, Name = "Curry Junction", FormerName = "", StreetAddress = "3549 Curry Lane", City = "Abilene", Market = "Abilene", State = "TX"},
                    new Property() {PropertyID = 85631, Name = "Riatta Ranch", FormerName = "", StreetAddress = "1111 Musken", City = "Abilene", Market = "Abilene", State = "TX"}                 
                }
            }; 
        }        

    }
}