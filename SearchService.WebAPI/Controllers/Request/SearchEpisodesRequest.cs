using FluentValidation;

namespace SearchService.WebAPI.Controllers.Request;
public record SearchEpisodesRequest(string keyWord, int pageIndex, int pageSize);

public class SearchEpisodesRequestValidator : AbstractValidator<SearchEpisodesRequest>
{
    public SearchEpisodesRequestValidator()
    {
        RuleFor(e => e.keyWord).NotNull().MinimumLength(2).MaximumLength(100);
        RuleFor(e => e.pageIndex).GreaterThan(0);//页号从1开始
        RuleFor(e => e.pageSize).GreaterThanOrEqualTo(5);
    }
}
