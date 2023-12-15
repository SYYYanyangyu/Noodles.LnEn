
using FluentValidation;

namespace Listening.Admin.WebAPI.Categories.Request;

//启用了<Nullable>enable</Nullable>，所以string ChineseName就是非可空，会自动校验
public record CategoryAddRequest(MultilingualString Name, Uri CoverUrl,string path);

public class CategoryAddRequestValidator : AbstractValidator<CategoryAddRequest>
{
    public CategoryAddRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name.Chinese).NotNull().Length(1, 200);
        RuleFor(x => x.Name.English).NotNull().Length(1, 200);
        RuleFor(x => x.CoverUrl); //CoverUrl允许为空
    }
}