using NUnit.Framework;
using SmartAppModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAppUnitTests
{
    [TestFixture]
    public class SmartAppSearchClientTest
    {
        //Declare global variables here
        const string mainServiceUrl = "https://localhost:5001/api/smartSearch";  

        [Test]
        public async Task GetSearchResultsForAllUSATestOK()
        {
            string serviceRequest = $"{mainServiceUrl}?SearchPhase=Essex&Limit=20&Markets=";    //Final service URL to be called
            var serviceResponse = await GetSearchResults(serviceRequest);
            Assert.IsTrue(serviceResponse != null);
        }

        [SetUp]
        public void Setup()
        {
        }

        //************ Temporary Search Results (Mock) **************
        private Task<SearchedItems> GetSearchResults(string request)  
        {
            return Task.Run<SearchedItems>(() => new SearchedItems { 
                    ManagementsFound = new List<Management>(){ 
                        new Management(){ MgmtID = 27918, Name = "Essex Property Trust AKA Essex Apartment Homes", Market = "San Francisco" },
                        new Management(){ MgmtID = 24736, Name = "Privately Owned and Managed", Market = "San Francisco" }  
                    },
                    PropertiesFound = new List<Property>(){
                        new Property(){ PropertyID = 85630, Name = "Curry Junction", FormerName = "", StreetAddress = "3549 Curry Lane", City = "Abilene", Market = "Abilene", State = "TX"},
                        new Property(){ PropertyID = 85631, Name = "Riatta Ranch", FormerName = "", StreetAddress = "1111 Musken", City = "Abilene", Market = "Abilene", State = "TX"}                
                    }
                }
            );
        }

    }
}