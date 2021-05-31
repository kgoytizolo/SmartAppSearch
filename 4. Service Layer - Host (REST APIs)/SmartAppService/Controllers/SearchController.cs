using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartAppModels;
using SmartAppService.Interfaces;
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
        /// This REST service Api will return a customized Management and/or Properties market search result from AWS ElasticSearch
        /// Format: HTTP/1.1 GET https://[Server]/api/smartSearch?SearchPhase=Essex&Limit=20&Markets=
        /// </summary>
        /// <param name="searchParams">Contains the following input queries for filtering: </param>
        /// {searchPhrase: string}   (Required) 
        /// {limit: int}            (Default value = 25)
        /// {market: string[]}      (Optional - If there's not data retrieved, searchs through all USA)
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SearchedItems))]
        [ProducesResponseType(400)]
        public async Task<SearchedItems> Get([FromQuery] SearchInputParams searchParams)
        {
            _logger.LogInformation("Entering into managements and/or properties search..");       
            (bool isValidationOk, string validationMessage) validationResult = _searchValidator.Validate<SearchInputParams>(searchParams);
            return await _searchRepository.GetResultsFromSearch(searchParams);
        }
    }
}
