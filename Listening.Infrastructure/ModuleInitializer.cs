using Listening.Domain;
using Microsoft.Extensions.DependencyInjection;
using Noodles.Common;

namespace Listening.Infrastructure;

public class ModuleInitializer: IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IListeningRepository, ListeningRepository>();
    }
}