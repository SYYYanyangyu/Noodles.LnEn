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
        private readonly IWebHostEnvironment environment;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="domainService"></param>
        /// <param name="repository"></param>
        /// <param name="dbContext"></param>
        public UploaderController(IWebHostEnvironment environment, FSDomainService domainService, IFSRepository repository, FSDbContext dbContext)
        {
            this.domainService = domainService;
            this.dbContext = dbContext;
            this.repository = repository;
            this.environment = environment;
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

        [HttpPost]
        public async Task<IActionResult> UploadImages([FromForm] IFormFile file)
        {
            try
            {
                // 确保文件内容不能为为空
                if (file == null || file.Length == 0)
                    return BadRequest("file is nor present or empty");

                // 生成文件保存路径 ， 保存到wwwroot/uploads 下
                var filePath = Path.Combine(environment.WebRootPath, "uploads", file.FileName);

                // 确保目录已经存在
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // 保存文件到路径
                await using var stream = System.IO.File.Create(filePath);
                await file.CopyToAsync(stream);

                // 返回文件的访问URL
                var url = $"{Request.Scheme}://{Request.Host.Value}/uploads/{file.FileName}";

                return Ok(new { url });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
