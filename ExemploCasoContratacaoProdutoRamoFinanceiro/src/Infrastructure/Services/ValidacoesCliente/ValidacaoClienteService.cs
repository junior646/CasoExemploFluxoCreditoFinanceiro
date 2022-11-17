using System.Net.Http.Json;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Common.Interfaces;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.ValidacoesCliente.ConsultarBloqueiosCliente;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.ValidacoesCliente.ConsultarLiberacaoCreditoCliente;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.ValidacoesCliente.ConsultarServicosContratadosCliente;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Services.ValidacoesCliente;
internal class ValidacaoClienteService : IValidacaoClienteService
{
    private readonly HttpClient _httpClientValidarCliente;
    public ValidacaoClienteService(HttpClient httpClientValidarCliente)
    {
        _httpClientValidarCliente = httpClientValidarCliente;
    }

    public async Task<ConsultarBloqueiosClienteResponse?> ConsultarBloqueiosCliente(ConsultarBloqueiosClienteRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClientValidarCliente.PostAsJsonAsync("consultar-bloqueios", request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var deserializeContent = await response.Content.ReadFromJsonAsync<ConsultarBloqueiosClienteResponse>(cancellationToken: cancellationToken);

            return deserializeContent;
        }
        else
        {
            if (request.CPF == 12345678909)
                //Simulando retorno do que deveria ser uma API de validações
                return new() { };
        }

        return default;
    }

    public async Task<ConsultarLiberacaoCreditoClienteResponse?> ConsultarLiberacaoCreditoCliente(ConsultarLiberacaoCreditoClienteRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClientValidarCliente.PostAsJsonAsync("consultar-liberacao-credito", request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var deserializeContent = await response.Content.ReadFromJsonAsync<ConsultarLiberacaoCreditoClienteResponse>(cancellationToken: cancellationToken);

            return deserializeContent;
        }
        else
        {
            if (request.CPF == 12345678909)
                //Simulando retorno do que deveria ser uma API de validações
                return new() {
                    PossuiCreditoLiberado = true
                };
        }

        return default;
    }

    public async Task<ConsultarServicosContratadosClienteResponse?> ConsultarServicosContratadosCliente(ConsultarServicosContratadosClienteRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClientValidarCliente.PostAsJsonAsync("consultar-servicos-contratados", request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var deserializeContent = await response.Content.ReadFromJsonAsync<ConsultarServicosContratadosClienteResponse>(cancellationToken: cancellationToken);

            return deserializeContent;
        }
        else
        {
            if (request.CPF == 12345678909)
                //Simulando retorno do que deveria ser uma API de validações
                return new() {
                    
                };
        }

        return default;
    }
}