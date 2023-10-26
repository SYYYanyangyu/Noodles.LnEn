using FluentValidation;
using Noodles.Common.Validators;

namespace Listening.Admin.WebAPI.Controllers.Albums.Request;


public class AlbumsSortRequest
{
    public Guid[] SortedAlbumIds { get; set; }
}

public class AlbumsSortRequestValidator : AbstractValidator<AlbumsSortRequest>
{
    public AlbumsSortRequestValidator()
    {
        RuleFor(r => r.SortedAlbumIds).NotNull().NotEmpty().NotContains(Guid.Empty)
            .NotDuplicated();
    }
}