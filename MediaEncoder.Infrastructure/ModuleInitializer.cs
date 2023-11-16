using MediaEncoder.Domain;
using Microsoft.Extensions.DependencyInjection;
using Noodles.Common;

namespace MediaEncoder.Infrastructure;

public class ModuleInitializer: IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IMediaEncoderRepository, MediaEncoderRepository>();
        services.AddScoped<IMediaEncoder, ToM4AEncoder>();
    }
}