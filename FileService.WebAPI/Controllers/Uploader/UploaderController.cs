using FileService.Domain;
using FileService.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noodles.ASPNETCore;

namespace FileService.WebAPI.Controllers.Uploader
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]    // 需要权限验证
    [UnitOfWork(typeof(FSDbContext))]
    public class UploaderController : ControllerBase
    {
        private readonly FSDbContext dbContext;
        private readonly FSDomainService domainService;
        private readonly IFSRepository repository;
        public UploaderController()
        {
            this.domainService = domainService;
            this.dbContext = dbContext;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<FileExistsResponse> FileExists(long fileSize, string sha256Hash)
        {
            try
            {
                var item = await repository.FindFileAsync(fileSize, sha256Hash);
                if (item == null)
                {
                    return new FileExistsResponse(false, null);
                }
                else
                {
                    return new FileExistsResponse(true, item.RemoteUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Uri>> Upload([FromForm] UploadRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var file = request.File;
                string fileName = file.FileName;
                using Stream stream = file.OpenReadStream();
                var upItem = await domainService.UploadAsync(stream, fileName, cancellationToken);
                dbContext.Add(upItem);
                return upItem.RemoteUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
