using FluentValidation;
using Listening.Infrastructure;

namespace Listening.Admin.WebAPI.Controllers.Albums.Request;

public record AlbumAddRequest(MultilingualString Name, Guid CategoryId);

//把校验规则写到单独的文件，也是DDD的一种原则
public class AlbumAddRequestValidator : AbstractValidator<AlbumAddRequest>
{
    public AlbumAddRequestValidator(ListeningDbContext dbCtx)
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name.Chinese).NotNull().Length(1, 200);
        RuleFor(x => x.Name.English).NotNull().Length(1, 200);
        ///验证CategoryId是否存在
        // RuleFor(x => x.CategoryId).Must((cId, ct) => dbCtx.Query<Category>().Any(c => c.Id == cId))
        //     .WithMessage(c => $"CategoryId={c.CategoryId}不存在");
    }
}