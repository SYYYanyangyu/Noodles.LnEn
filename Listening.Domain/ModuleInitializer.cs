using Microsoft.Extensions.DependencyInjection;
using Noodles.Common;

namespace Listening.Domain
{
    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ListeningDomainService>();
        }
    }
}
