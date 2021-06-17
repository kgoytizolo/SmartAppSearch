using GenericErrorHandler;
using MediatR;

namespace SmartAppCore.Features.Searchs.GetSearchResults
{
    public class GetSearchResultsQuery : IRequest<Response<SearchedItemsViewModels>>
    {
        public string SearchPhrase { get ; set; }    //Required
        public int Limit { get; set; }              //Default = 25 results per search
        public string[] Markets { get; set; }       //Optional
    }  
}