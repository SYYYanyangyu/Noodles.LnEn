using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace FileService.WebAPI.Controllers.Uploader;

public class UploadRequest
{
    public IFormFile File { get; set; }
}

public class UploadRequestValidator : AbstractValidator<UploadRequest>
{
    public UploadRequestValidator()
    {
        try
        {
            long maxFileSize = 50 * 1024 * 1024;//最大文件大小
            RuleFor(e => e.File).NotNull().Must(ff => ff.Length > 0 && ff.Length < maxFileSize);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

