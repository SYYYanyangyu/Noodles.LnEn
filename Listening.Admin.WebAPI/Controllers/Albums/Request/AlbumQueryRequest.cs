using FluentValidation;
using Listening.Infrastructure;


public record AlbumQueryRequest(int pageIndex, int pageSize , string search);

//把校验规则写到单独的文件，也是DDD的一种原则
public class AlbumAddRequestValidator : AbstractValidator<AlbumQueryRequest>
{
    public AlbumAddRequestValidator(ListeningDbContext dbCtx)
    {
        RuleFor(x => x.search).NotNull().MinimumLength(2).MaximumLength(100);
        RuleFor(x => x.pageIndex).NotEmpty();
        RuleFor(x => x.pageSize).NotNull().InclusiveBetween(1, 1000);
    }
}

