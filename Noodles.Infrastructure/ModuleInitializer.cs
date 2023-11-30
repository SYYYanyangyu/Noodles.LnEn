using Microsoft.Extensions.DependencyInjection;
using Noodles.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noodles.Infrastructure
{
    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            //因为是泛型，所以不能用AddScoped<IDbContextRepository<>,typeof(DbContextRepository<>>这种方式注册
            //services.AddScoped(typeof(IDbContextRepository<>), typeof(DbContextRepository<>));
        }
    }
}
