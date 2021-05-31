using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartAppModels;
using SmartAppService.Interfaces;
using SmartAppRepository.Interfaces;

namespace SmartAppService.Controllers
{
    [ApiController]
    [Route("Markets")]  
    public class MarketController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchRepository _searchRepository;
        static readonly string[] serviceScope = new string[] {"access_as_user"};

        public MarketController(ILogger<SearchController> logger, ISearchValidator searchValidator, 
                                    ISearchRepository searchRepository)
        {
            _logger = logger;
            _searchRepository = searchRepository;
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

        private IEnumerable<Market> GetMarkets(){
            List<Market> listOfMarkets = new List<Market>(){ 
                new Market(){ MarketStateCode = "CA", MarketState = "California", MarketsPerState = new string[]{"San Francisco"} },
                new Market(){ MarketStateCode = "GA", MarketState = "Georgia", MarketsPerState = new string[]{"Atlanta"} }                
             };
             return listOfMarkets.ToArray();
        }            

    }
}