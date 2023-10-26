
using FluentValidation;
using Noodles.Common.Validators;

namespace Listening.Admin.WebAPI.Controllers.Episodes;
public class EpisodesSortRequest
{
    public Guid[] SortedEpisodeIds { get; set; }
}

public class EpisodesSortRequestValidator : AbstractValidator<EpisodesSortRequest>
{
    public EpisodesSortRequestValidator()
    {
        RuleFor(r => r.SortedEpisodeIds).NotNull().NotEmpty().NotContains(Guid.Empty).NotDuplicated();
    }
}
