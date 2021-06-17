using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SmartAppCore.Interfaces.Repository;
using AutoMapper;
using GenericErrorHandler;

namespace SmartAppCore.Features.Searchs.GetSearchResults
{
    public class GetSearchResultsQueryHandler : IRequestHandler<GetSearchResultsQuery, Response<SearchedItemsViewModels>>
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IMapper _mapper;

        public GetSearchResultsQueryHandler(IMapper mapper, ISearchRepository searchRepository)
        {
            _mapper = mapper;
            _searchRepository = searchRepository;
        }

        public async Task<Response<SearchedItemsViewModels>> Handle(GetSearchResultsQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<SearchedItemsViewModels>();
            var requestValidator = new GetSearchResultsValidator();
            var validatorResult = await requestValidator.ValidateAsync(request);

            if(validatorResult.Errors.Count > 0){
                if(validatorResult.Errors.Exists(x => x.ErrorCode.Equals("9400"))){
                    response.SetErrorInfo(9400, "Search phrase is required", "");
                }                
                if(validatorResult.Errors.Exists(x => x.ErrorCode.Equals("1400"))){
                    request.Limit = 25;     //Limit number which is less than 25 will be redefined as 25 by default    
                }
            }

            if(response.Success){
                var searchResults = await _searchRepository.GetResultsFromSearchWithResponse(request.SearchPhrase, 
                                                                                            request.Limit,
                                                                                            request.Markets);
                response.ResponseItem.ManagementsFound = _mapper.Map<List<ManagementDto>>(searchResults.ResponseItem.ManagementsFound);
                response.ResponseItem.PropertiesFound = _mapper.Map<List<PropertyDto>>(searchResults.ResponseItem.PropertiesFound);
            }

            return response;
        }
    }
}