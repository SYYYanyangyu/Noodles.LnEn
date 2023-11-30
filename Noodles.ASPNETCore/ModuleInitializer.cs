using Microsoft.Extensions.DependencyInjection;
using Noodles.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noodles.ASPNETCore
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
            services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();
        }
    }
}
