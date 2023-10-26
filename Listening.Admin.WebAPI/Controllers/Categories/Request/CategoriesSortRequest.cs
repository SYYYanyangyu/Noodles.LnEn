
using FluentValidation;
using Noodles.Common.Validators;

namespace Listening.Admin.WebAPI.Controllers.Categories.Request;
public class CategoriesSortRequest
{
    /// <summary>
    /// 排序后的类别Id
    /// </summary>
    public Guid[] SortedCategoryIds { get; set; }
}

public class CategoriesSortRequestValidator : AbstractValidator<CategoriesSortRequest>
{
    public CategoriesSortRequestValidator()
    {
        RuleFor(r => r.SortedCategoryIds).NotNull().NotEmpty().NotContains(Guid.Empty)
            .NotDuplicated();
    }
}