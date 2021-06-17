using FluentValidation;

namespace SmartAppCore.Features.Searchs.GetSearchResults
{
    /// <summary>
    /// This class contains several business rules to validate the full text search input query
    /// </summary>
    public class GetSearchResultsValidator : AbstractValidator<GetSearchResultsQuery>
    {
        public GetSearchResultsValidator()
        {
            RuleFor(sp => sp.SearchPhrase)
                .NotEmpty().WithMessage("Input {PropertyName} is required.").WithErrorCode("9400")
                .NotNull().WithMessage("Input {PropertyName} is required.").WithErrorCode("9400");
            RuleFor(l => l.Limit)
                .LessThan(25)
                .WithMessage("Input {PropertyName} with a value less than 25 should be reassigned to 25 as default")
                .WithErrorCode("1400");                
        }
    }  
}