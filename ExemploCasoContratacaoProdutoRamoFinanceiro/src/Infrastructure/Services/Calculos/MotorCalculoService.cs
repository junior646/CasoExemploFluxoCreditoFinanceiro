using System.Net.Http.Json;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Common.Interfaces;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoDireto;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoConsignado;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoImobiliario;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoEmprestimoPessoaFisica;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoEmprestimoPessoaJuridica;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Services.Calculations;

internal class MotorCalculoService : IMotorCalculoService
{
    private readonly HttpClient _httpClientMotorCalculo;

    public MotorCalculoService(HttpClient httpClientMotorCalculo)
    {
        _httpClientMotorCalculo = httpClientMotorCalculo;
    }

    public async Task<ObterCalculoCreditoConsignadoResponse?> ObterCalculoConsignado(ObterCalculoCreditoConsignadoRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClientMotorCalculo.PostAsJsonAsync("calcular-consignado", request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var deserializeContent = await response.Content.ReadFromJsonAsync<ObterCalculoCreditoConsignadoResponse>(cancellationToken: cancellationToken);

            return deserializeContent;
        }
        else
        {
            var valorTotalCredito = request.ValorCreditoSolicitado + ((request.ValorCreditoSolicitado * (request.TaxaJuros * request.QuantidadeParcelas)) / 100);
            if (request.ValorCreditoSolicitado > 0)
                //Simulando retorno do que deveria ser uma API de cálculos
                return new()
                {
                    DataPrimeiraParcela = request.DataPrimeiroPagamento,
                    DataUltimaParcela = request.DataPrimeiroPagamento.AddMonths(request.QuantidadeParcelas),
                    ValorCalculo = valorTotalCredito,
                    ValorPorParcela = valorTotalCredito / request.QuantidadeParcelas,
                    ValorPrimeiraParcela = valorTotalCredito / request.QuantidadeParcelas,
                    ValorUltimaParcela = valorTotalCredito / request.QuantidadeParcelas
                };
        }

        return default;
    }

    public async Task<ObterCalculoCreditoDiretoResponse?> ObterCalculoDireto(ObterCalculoCreditoDiretoRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClientMotorCalculo.PostAsJsonAsync("calcular-direto", request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var deserializeContent = await response.Content.ReadFromJsonAsync<ObterCalculoCreditoDiretoResponse>(cancellationToken: cancellationToken);

            return deserializeContent;
        }
        else
        {
            var valorTotalCredito = request.ValorCreditoSolicitado + ((request.ValorCreditoSolicitado * (request.TaxaJuros * request.QuantidadeParcelas)) / 100);
            if (request.ValorCreditoSolicitado > 0)
                //Simulando retorno do que deveria ser uma API de cálculos
                return new()
                {
                    DataPrimeiraParcela = request.DataPrimeiroPagamento,
                    DataUltimaParcela = request.DataPrimeiroPagamento.AddMonths(request.QuantidadeParcelas),
                    ValorCalculo = valorTotalCredito,
                    ValorPorParcela = valorTotalCredito / request.QuantidadeParcelas,
                    ValorPrimeiraParcela = valorTotalCredito / request.QuantidadeParcelas,
                    ValorUltimaParcela = valorTotalCredito / request.QuantidadeParcelas
                };
        }

        return default;
    }

    public async Task<ObterCalculoCreditoImobiliarioResponse?> ObterCalculoImobiliario(ObterCalculoCreditoImobiliarioRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClientMotorCalculo.PostAsJsonAsync("calcular-imobiliario", request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var deserializeContent = await response.Content.ReadFromJsonAsync<ObterCalculoCreditoImobiliarioResponse>(cancellationToken: cancellationToken);

            return deserializeContent;
        }
        else
        {
            var valorTotalCredito = request.ValorCreditoSolicitado + ((request.ValorCreditoSolicitado * (request.TaxaJuros * request.QuantidadeParcelas)) / 100);
            if (request.ValorCreditoSolicitado > 0)
                //Simulando retorno do que deveria ser uma API de cálculos
                return new()
                {
                    DataPrimeiraParcela = request.DataPrimeiroPagamento,
                    DataUltimaParcela = request.DataPrimeiroPagamento.AddMonths(request.QuantidadeParcelas),
                    ValorCalculo = valorTotalCredito,
                    ValorPorParcela = valorTotalCredito / request.QuantidadeParcelas,
                    ValorPrimeiraParcela = valorTotalCredito / request.QuantidadeParcelas,
                    ValorUltimaParcela = valorTotalCredito / request.QuantidadeParcelas
                };
        }

        return default;
    }

    public async Task<ObterCalculoCreditoEmprestimoPessoaFisicaResponse?> ObterCalculoEmprestimoPessoaFisica(ObterCalculoCreditoEmprestimoPessoaFisicaRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClientMotorCalculo.PostAsJsonAsync("calcular-emprestimo/pessoa-fisica", request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var deserializeContent = await response.Content.ReadFromJsonAsync<ObterCalculoCreditoEmprestimoPessoaFisicaResponse>(cancellationToken: cancellationToken);

            return deserializeContent;
        }
        else
        {
            var valorTotalCredito = request.ValorCreditoSolicitado + ((request.ValorCreditoSolicitado * (request.TaxaJuros * request.QuantidadeParcelas)) / 100);
            if (request.ValorCreditoSolicitado > 0)
                //Simulando retorno do que deveria ser uma API de cálculos
                return new()
                {
                    DataPrimeiraParcela = request.DataPrimeiroPagamento,
                    DataUltimaParcela = request.DataPrimeiroPagamento.AddMonths(request.QuantidadeParcelas),
                    ValorCalculo = valorTotalCredito,
                    ValorPorParcela = valorTotalCredito / request.QuantidadeParcelas,
                    ValorPrimeiraParcela = valorTotalCredito / request.QuantidadeParcelas,
                    ValorUltimaParcela = valorTotalCredito / request.QuantidadeParcelas
                };
        }

        return default;
    }

    public async Task<ObterCalculoCreditoEmprestimoPessoaJuridicaResponse?> ObterCalculoEmprestimoPessoaJuridica(ObterCalculoCreditoEmprestimoPessoaJuridicaRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClientMotorCalculo.PostAsJsonAsync("calcular-emprestimo/pessoa-juridica", request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var deserializeContent = await response.Content.ReadFromJsonAsync<ObterCalculoCreditoEmprestimoPessoaJuridicaResponse>(cancellationToken: cancellationToken);

            return deserializeContent;
        }
        else
        {
            var valorTotalCredito = request.ValorCreditoSolicitado + ((request.ValorCreditoSolicitado * (request.TaxaJuros * request.QuantidadeParcelas)) / 100);
            if (request.ValorCreditoSolicitado > 0)
                //Simulando retorno do que deveria ser uma API de cálculos
                return new()
                {
                    DataPrimeiraParcela = request.DataPrimeiroPagamento,
                    DataUltimaParcela = request.DataPrimeiroPagamento.AddMonths(request.QuantidadeParcelas),
                    ValorCalculo = valorTotalCredito,
                    ValorPorParcela = valorTotalCredito / request.QuantidadeParcelas,
                    ValorPrimeiraParcela = valorTotalCredito / request.QuantidadeParcelas,
                    ValorUltimaParcela = valorTotalCredito / request.QuantidadeParcelas
                };
        }

        return default;
    }
}