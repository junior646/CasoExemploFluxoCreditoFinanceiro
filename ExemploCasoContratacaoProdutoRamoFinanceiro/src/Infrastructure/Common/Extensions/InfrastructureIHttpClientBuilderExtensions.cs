using Microsoft.Extensions.DependencyInjection;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Services.Http.MessageHandlers.Common;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Common.Extensions;
internal static class InfrastructureIHttpClientBuilderExtensions
{
    public static IHttpClientBuilder AddHttpApiClient<TClient, TImplementation>(this IServiceCollection services, string uriBase)
            where TClient : class
            where TImplementation : class, TClient
    {
        return services
            .AddHttpClient<TClient, TImplementation>(configureClient => configureClient.BaseAddress = new Uri(uriBase))
            .AddHttpMessageHandler<LoggingHttpMessageHandler>()
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { });
    }
}