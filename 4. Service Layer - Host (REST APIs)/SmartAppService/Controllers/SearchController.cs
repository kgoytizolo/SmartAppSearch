using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartAppModels;
using SmartAppService.Interfaces;
using SmartAppService.Validations;
using SmartAppRepository.Interfaces;

namespace SmartAppService.Controllers
{
    [ApiController]
    [Route("api/smartSearch")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchValidator _searchValidator;
        private readonly ISearchRepository _searchRepository;
        static readonly string[] serviceScope = new string[] {"access_as_user"};

        public SearchController(ILogger<SearchController> logger, ISearchValidator searchValidator, 
                                    ISearchRepository searchRepository)
        {
            _logger = logger;
            _searchValidator = searchValidator;
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

        /// <summary>
        /// This REST service Api will return a customized Management and/or Properties market search result from AWS ElasticSearch
        /// </summary>
        /// <param name="searchParams">Contains the following input queries for filtering: </param>
        /// {searchPhase: string}   (Required) 
        /// {limit: int}            (Default value = 25)
        /// {market: string[]}      (Optional - If there's not data retrieved, searchs through all USA)
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SearchedItems))]
        [ProducesResponseType(400)]
        public SearchedItems Get([FromQuery] SearchInputParams searchParams)
        {
            _logger.LogInformation("Entering into managements and/or properties search..");       
            (bool isValidationOk, string validationMessage) validationResult = _searchValidator.Validate<SearchInputParams>(searchParams);
            return _searchRepository.GetResultsFromSearch(searchParams);
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
