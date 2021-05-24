using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartAppModels;

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

        [HttpGet]
        public SearchedItems Get()
        {
            return searchResponse();        
        }

        //[HttpGet("searchAll/{searchPhase:alpha}/{param2:Guid}")]
        [HttpGet("{searchPhase:alpha}")]
        public SearchedItems Get(string searchPhase)
        {
            return searchResponse();
        }

        // [HttpGet("{searchPhase:string}")]
        // public IEnumerable<Management> Get(string searchPhase)
        // {
        //     return null;        
        // }

        // [HttpGet("{searchPhase:string}")]
        // public IEnumerable<Property> Get(string searchPhase)
        // {
        //     return null;        
        // }

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

    }
}
