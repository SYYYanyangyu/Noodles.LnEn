using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nest;
using SearchService.Domain;
using Noodles.Common;

namespace SearchService.Infrastructure
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            //services.AddHttpClient();
            services.AddScoped<IElasticClient>(sp =>
            {
                var option = sp.GetRequiredService<IOptions<ElasticSearchOptions>>();
                var settings = new ConnectionSettings(option.Value.Url).CertificateFingerprint("3aed30dfeffbb0d85e78ca491a572b77cfb0e4cdea7cf6a6075ae0c5399bb84f");
                return new ElasticClient(settings);
            });
            services.AddScoped<ISearchRepository, SearchRepository>();

        }
    }
}
