using Microsoft.Extensions.DependencyInjection;
using Noodles.Common;
using Noodles.Jwt;

namespace Noodles.Jwt;

    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }
    }

