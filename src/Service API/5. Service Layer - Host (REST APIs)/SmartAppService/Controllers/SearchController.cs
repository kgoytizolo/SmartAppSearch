using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartAppService.Models;
using SmartAppCore.Features.Searchs.GetSearchResults;
using MediatR;
using GenericErrorHandler;
using Microsoft.Extensions.Logging;

namespace SmartAppService.Controllers
{
    [ApiController]
    [Route("api/smartSearch")]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _searchMediator;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger, IMediator searchMediator)
        {
            _logger = logger;
            _searchMediator = searchMediator;
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
        [ProducesResponseType(200, Type = typeof(Response<SearchedItemsViewModels>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]    
        public async Task<IActionResult> Get([FromQuery] SearchInputParams searchParams)
        {
            var serviceResponse = new Response<SearchedItemsViewModels>();
            try{
                var serviceRequest = new GetSearchResultsQuery() { SearchPhrase = searchParams.SearchPhase, Limit = searchParams.Limit, Markets = searchParams.Markets };
                serviceResponse = await _searchMediator.Send(serviceRequest);
                switch (true) {
                    case true when serviceResponse.ErrorId == 0 && serviceResponse.Success: return Ok(serviceResponse);
                    case true when serviceResponse.ErrorId == 9400: return BadRequest(serviceResponse);
                    case true when serviceResponse.ErrorId == 9404: return NotFound(serviceResponse);
                    case true when (serviceResponse.ErrorId == 9000 || serviceResponse.ErrorId == 9999):                                            
                    default : 
                        if(serviceResponse.ErrorId != 9000 && serviceResponse.ErrorId != 9999) serviceResponse.SetErrorInfo(1000, "Unexpected error during the call of this service. Please, try again");
                        return StatusCode(500, serviceResponse); 
                };                               
            }
            catch(System.Exception e){
                _logger.LogError($"Internal Service Error - Code: {e.HResult}, Message: {e.Message}");
                serviceResponse.SetErrorInfo(10000, "Error during the call of this service. Please, try again");
                return StatusCode(500, serviceResponse); 
            }
        }

    }
}
