﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartAppModels;
using SmartAppService.Interfaces;
using SmartAppRepository.Interfaces;
using GenericErrorHandler;
using Microsoft.AspNetCore.Http;

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
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]    
        public async Task<IActionResult> Get([FromQuery] SearchInputParams searchParams)
        {
            var serviceResponse = new GenericErrorResponse<SearchedItems>();
            try{
                _logger.LogInformation("Entering into managements and/or properties search..");
                (bool isValidationOk, string validationMessage) validationResult = _searchValidator.Validate<SearchInputParams>(searchParams);
                if(validationResult.isValidationOk){
                    serviceResponse = await _searchRepository.GetResultsFromSearchWithResponse(searchParams);
                    if(serviceResponse.ErrorId == 9999)     return NotFound(serviceResponse);      //Resource not found - 404
                    return Ok(serviceResponse.ResponseItem);
                }
                else {
                    serviceResponse.SetErrorInfo(9400, validationResult.validationMessage);
                    return BadRequest(serviceResponse); 
                }
            }
            catch(System.Exception e){
                serviceResponse.SetErrorInfo(e.HResult, "Error during the call of this service. Please, try again");
                return StatusCode(500, serviceResponse);
            }
        }

    }
}
