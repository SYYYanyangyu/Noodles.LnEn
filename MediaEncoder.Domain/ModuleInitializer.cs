using Microsoft.Extensions.DependencyInjection;
using Noodles.Common;

namespace MediaEncoder.Domain;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<MediaEncoderFactory>();
    }
}