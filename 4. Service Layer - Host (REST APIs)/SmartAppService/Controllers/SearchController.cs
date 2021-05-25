using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartAppModels;
using SmartAppService.Models;

namespace SmartAppService.Controllers
{
    [ApiController]
    [Route("api/smartSearch")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        static readonly string[] serviceScope = new string[] {"access_as_user"};

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This REST service Api will return a list of markets organized by US state
        /// </summary>
        /// <returns>A list of markets per US state. Example: Markets in Georgia state => Atlanta, Augusta, etc.</returns>
        [HttpGet]
        [Route("Markets")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Market>))]
        [ProducesResponseType(400)]
        public IEnumerable<Market> Get()
        {
            return GetMarkets();
        }

        /// <summary>
        /// This REST service Api will return a customized Management and/or Properties market search result from AWS ElasticSearch
        /// </summary>
        /// <param name="searchPhase">(Required) </param>
        /// <param name="limit">(Default value = 25)</param>
        /// <param name="market">(Optional). In case of not data, it assumes that the search is applied to the entire US</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SearchedItems))]
        [ProducesResponseType(400)]
        public SearchedItems Get([FromQuery] SearchInputParams searchParams)
        {
            (bool isValidationOk, string validationMessage) validationResult = searchParams.Validate();
            _logger.LogInformation("Looking for search managements and/or properties..");
            return searchResponse();
        }

        //************ Temporary Private Methods ***************
        private SearchedItems searchResponse(){
            return new SearchedItems { 
                ManagementsFound = new List<Management>(){ 
                    new Management(){ MgmtID = 27918, Name = "Essex Property Trust AKA Essex Apartment Homes", Market = "San Francisco" },
                    new Management(){ MgmtID = 24736, Name = "Privately Owned and Managed", Market = "San Francisco" }  
                },
                PropertiesFound = new List<Property>(){
                    new Property(){ PropertyID = 85630, Name = "Curry Junction", FormerName = "", StreetAddress = "3549 Curry Lane", City = "Abilene", Market = "Abilene", State = "TX"},
                    new Property(){ PropertyID = 85631, Name = "Riatta Ranch", FormerName = "", StreetAddress = "1111 Musken", City = "Abilene", Market = "Abilene", State = "TX"}                
                }
            };            
        }

        private IEnumerable<Market> GetMarkets(){
            List<Market> listOfMarkets = new List<Market>(){ 
                new Market(){ MarketStateCode = "CA", MarketState = "California", MarketsPerState = new string[]{"San Francisco"} },
                new Market(){ MarketStateCode = "GA", MarketState = "Georgia", MarketsPerState = new string[]{"Atlanta"} }                
             };
             return listOfMarkets.ToArray();
        }

    }
}
