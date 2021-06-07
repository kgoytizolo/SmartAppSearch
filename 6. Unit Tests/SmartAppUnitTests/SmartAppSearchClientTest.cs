using NUnit.Framework;
using SmartAppModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;

namespace SmartAppUnitTests
{
    [TestFixture]
    public class SmartAppSearchClientTest
    {
        //Declare global variables here
        const string mainServiceUrl = "https://localhost:5001/api/smartSearch";
        RestClient serviceClientSmartAppSearch;

        [OneTimeSetUp]
        public void Init()
        {
            serviceClientSmartAppSearch = new RestClient(mainServiceUrl);
            serviceClientSmartAppSearch.Timeout = 9000;
        }

        [TestCase("stones and rocks", 20, new string[] {"Houston"}, ExpectedResult = true)]
        [TestCase("stones & rocks", 20, new string[] {"Houston"}, ExpectedResult = true)]
        [TestCase("<p>stones and rocks</p>", 20, new string[] {"Houston"}, ExpectedResult = true)]
        [TestCase("<p>stones & rocks</p>", 20, new string[] {"Houston"}, ExpectedResult = true)]        
        [TestCase("La frontera", 20, new string[] {"Austin"}, ExpectedResult = true)]        
        [TestCase("La frontera district", 20, new string[] {"Austin"}, ExpectedResult = true)]                        
        [TestCase("Villas of St. Moritz", 20, new string[] {"San Antonio"}, ExpectedResult = true)]                        
        [TestCase("Villas of saint Moritz", 20, new string[] {"San Antonio"}, ExpectedResult = true)]                                
        public bool GetSearchResultsForOneMarket_OK_Test(string searchPhrase, int limit, string[] markets)
        {
            return GetSearchResultsRestApi(searchPhrase, limit, markets);
        }

        [TestCase("stones and rocks", 20, new string[] {"Atlanta", "Houston"}, ExpectedResult = true)]
        [TestCase("stones & rocks", 20, new string[] {"Atlanta", "Houston"}, ExpectedResult = true)]
        [TestCase("<p>stones and rocks</p>", 20, new string[] {"Atlanta", "Houston"}, ExpectedResult = true)]
        [TestCase("<p>stones & rocks</p>", 20, new string[] {"Atlanta", "Houston"}, ExpectedResult = true)]   
        [TestCase("La frontera", 20, new string[] {"Austin", "Orange County"}, ExpectedResult = true)]        
        [TestCase("La frontera district", 20, new string[] {"Austin", "Orange County"}, ExpectedResult = true)]                        
        [TestCase("St. Moritz", 20, new string[] {"San Antonio", "San Francisco"}, ExpectedResult = true)]                        
        [TestCase("saint Moritz", 20, new string[] {"San Antonio", "San Francisco"}, ExpectedResult = true)]
        [Ignore("Just for now")] 
        public bool GetSearchResultsForSeveralMarkets_OK_Test(string searchPhrase, int limit, string[] markets)
        {
            return GetSearchResultsRestApi(searchPhrase, limit, markets);
        }

        [TestCase("stones and rocks", 20, new string[] {}, ExpectedResult = true)]
        [TestCase("stones & rocks", 20, new string[] {}, ExpectedResult = true)]
        [TestCase("<p>stones and rocks</p>", 20, new string[] {}, ExpectedResult = true)]
        [TestCase("<p>stones & rocks</p>", 20, new string[] {}, ExpectedResult = true)]   
        [TestCase("La frontera", 20, new string[] {}, ExpectedResult = true)]        
        [TestCase("La frontera district", 20, new string[] {}, ExpectedResult = true)]                        
        [TestCase("St. Moritz", 20, new string[] {}, ExpectedResult = true)]                        
        [TestCase("saint Moritz", 20, new string[] {}, ExpectedResult = true)]
        [Ignore("Just for now")] 
        public bool GetSearchResultsForAllMarkets_OK_Test(string searchPhrase, int limit, string[] markets)
        {
            return GetSearchResultsRestApi(searchPhrase, limit, markets);
        }     

        /// <summary>
        /// Evaluates if the REST API evaluates in case of the required input value is null or empty.
        /// Response returned should be 400 - Bad request
        /// </summary>
        [Ignore("Just for now")] 
        [Test]
        public void EvaluateInputRequiredSearchParam_Test()
        {
            var request = new RestRequest(Method.GET);
            var requestParams = new SearchInputParams(){SearchPhase = "", Limit = 15, Markets = new string[] {}};
            request.AddJsonBody(requestParams);
            var response = serviceClientSmartAppSearch.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Pass();
        }

        [Ignore("Just mock")]
        [Test]
        public async Task GetSearchResultsForAllUSATestOK()
        {
            string serviceRequest = $"{mainServiceUrl}?SearchPhase=Essex&Limit=20&Markets=";    //Final service URL to be called
            var serviceResponse = await GetSearchResults(serviceRequest);
            Assert.IsTrue(serviceResponse != null);
        }

        //************ Private methods ******************************
        private bool GetSearchResultsRestApi(string searchPhrase, int limit, string[] markets)
        {
            var request = new RestRequest(Method.GET);
            var requestParams = new SearchInputParams(){SearchPhase = searchPhrase, Limit = limit, Markets = markets};
            request.AddJsonBody(requestParams);
            var response = serviceClientSmartAppSearch.Execute(request);
            return (response.StatusCode == HttpStatusCode.OK && response.ContentType.StartsWith("application/json"));
        }        

        //************ Temporary Search Results (Mock) **************
        private Task<SearchedItems> GetSearchResults(string request)  
        {
            return Task.Run<SearchedItems>(() => new SearchedItems { 
                ManagementsFound = new List<Managements>(){ 
                    new Managements(){ Mgmt = { MgmtID = 27918, Name = "Essex Property Trust AKA Essex Apartment Homes", Market = "San Francisco" } },
                    new Managements(){ Mgmt = { MgmtID = 24736, Name = "Privately Owned and Managed", Market = "San Francisco" } } 
                },
                PropertiesFound = new List<SmartAppModels.Properties>(){
                    new SmartAppModels.Properties(){ Property = new Property {PropertyID = 85630, Name = "Curry Junction", FormerName = "", StreetAddress = "3549 Curry Lane", City = "Abilene", Market = "Abilene", State = "TX"}},
                    new SmartAppModels.Properties(){ Property = new Property {PropertyID = 85631, Name = "Riatta Ranch", FormerName = "", StreetAddress = "1111 Musken", City = "Abilene", Market = "Abilene", State = "TX"}}                 
                }
            }
            );
        }

    }
}