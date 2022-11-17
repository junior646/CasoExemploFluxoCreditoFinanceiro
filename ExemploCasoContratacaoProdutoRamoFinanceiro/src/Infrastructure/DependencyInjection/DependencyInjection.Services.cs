using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Common.Interfaces;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.DependencyInjection;
public static partial class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}