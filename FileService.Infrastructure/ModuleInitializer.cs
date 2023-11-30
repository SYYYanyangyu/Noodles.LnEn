using FileService.Domain;
using FileService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Noodles.Common;

namespace FileService.Infrastructure
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void  Initialize(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            // 类似于文件夹，磁盘共享
            services.AddScoped<IStorageClient, SMBStorageClient>();
            // 添加云存储服务
            //services.AddScoped<IStorageClient, UpYunStorageClient>();
            services.AddScoped<IStorageClient, MockCloudStorageClient>();
            services.AddScoped<IFSRepository, FSRepository>();
            services.AddScoped<FSDomainService>();
            services.AddHttpClient();
        }
    }
}
