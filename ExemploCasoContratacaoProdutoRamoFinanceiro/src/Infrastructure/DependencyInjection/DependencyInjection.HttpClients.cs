using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Common.Interfaces;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Common.Extensions;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Services.Calculations;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Configuration.AppSettings.Mappings;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Services.ValidacoesCliente;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Services.Http.MessageHandlers.Common;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var configuracaoServices = configuration
            .GetSection(nameof(ConfiguracaoServices))
            .Get<ConfiguracaoServices>();

        services.AddTransient<LoggingHttpMessageHandler>();
        services.AddHttpApiClient<IMotorCalculoService, MotorCalculoService>(configuracaoServices.MotorCalculoService.UriBase);
        services.AddHttpApiClient<IValidacaoClienteService, ValidacaoClienteService>(configuracaoServices.ValidacaoClienteService.UriBase);

        return services;
    }
}