using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.DependencyInjection;
public static partial class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddHttpClients(configuration);

        return services;
    }
}