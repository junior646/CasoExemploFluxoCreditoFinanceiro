using MediatR;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Domain.Enums;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Common.Interfaces;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoDireto;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoConsignado;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoImobiliario;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoEmprestimoPessoaFisica;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoEmprestimoPessoaJuridica;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.ValidacoesCliente.ConsultarLiberacaoCreditoCliente;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.ValidacoesCliente.ConsultarBloqueiosCliente;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.ValidacoesCliente.ConsultarServicosContratadosCliente;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.UseCases.ContratacaoCredito.Queries.ConsultarCredito;

public class ConsultarCreditoQueryHandler : IRequestHandler<ConsultarCreditoQuery, ConsultarCreditoQueryResponse>
{
    private readonly IMotorCalculoService _calculationManagerService;
    private readonly IValidacaoClienteService _validacaoClienteService;

    public ConsultarCreditoQueryHandler(IMotorCalculoService calculationManagerService, IValidacaoClienteService validacaoClienteService)
    {
        _calculationManagerService = calculationManagerService;
        _validacaoClienteService = validacaoClienteService;
    }

    public async Task<ConsultarCreditoQueryResponse> Handle(ConsultarCreditoQuery request, CancellationToken cancellationToken)
    {
        ConsultarCreditoQueryResponse? response;

        switch (request.TipoCredito)
        {
            case TipoCredito.Consignado:
                response = await ObterCalculoConsignado(request, cancellationToken);
                break;
            case TipoCredito.Direto:
                response = await ObterCalculoDireto(request, cancellationToken);
                break;
            case TipoCredito.PessoaFisica:
                response = await ObterCalculoEmprestimoPessoaFisica(request, cancellationToken);
                break;
            case TipoCredito.PessoaJuridica:
                response = await ObterCalculoEmprestimoPessoaJuridica(request, cancellationToken);
                break;
            case TipoCredito.Imobiliario:
                response = await ObterCalculoImobiliario(request, cancellationToken);
                break;
            default:
                return new ConsultarCreditoQueryResponse();
        }

        if (response is null)
            return new ConsultarCreditoQueryResponse();

        //TODO: Será necessário receber no request um dado sensível complementar (CPF ou CNPJ) para validar a liberação de crédito

        var permitirContratacao = await ValidarLiberacaoCredito(12345678909, cancellationToken);

        if (permitirContratacao is StatusCredito.Recusado)
            return new ConsultarCreditoQueryResponse();

        response.PreencherStatusCredito(permitirContratacao);

        return response;
    }

    internal async Task<StatusCredito> ValidarLiberacaoCredito(long cpfCnpj, CancellationToken cancellationToken)
    {
        StatusCredito resultadoValidacaoCredito = StatusCredito.Recusado;
        long cpf = 0, cnpj = 0;

        if (cpfCnpj.ToString().Length > 11)
            cnpj = cpfCnpj;
        else
            cpf = cpfCnpj;

        var resultConsultarBloqueiosCliente = await _validacaoClienteService.ConsultarBloqueiosCliente(new ConsultarBloqueiosClienteRequest { CPF = cpf, CNPJ = cnpj }, cancellationToken);
        if (resultConsultarBloqueiosCliente is null || resultConsultarBloqueiosCliente.PossuiBloqueios)
            return resultadoValidacaoCredito;

        var resultConsultarLiberacaoCreditoCliente = await _validacaoClienteService.ConsultarLiberacaoCreditoCliente(new ConsultarLiberacaoCreditoClienteRequest { CPF = cpf, CNPJ = cnpj }, cancellationToken);
        if (resultConsultarLiberacaoCreditoCliente is null || resultConsultarLiberacaoCreditoCliente.PossuiCreditoLiberado is false)
            return resultadoValidacaoCredito;

        var resultConsultarServicosContratadosCliente = await _validacaoClienteService.ConsultarServicosContratadosCliente(new ConsultarServicosContratadosClienteRequest { CPF = cpf, CNPJ = cnpj }, cancellationToken);
        if (resultConsultarServicosContratadosCliente is null || resultConsultarServicosContratadosCliente.PossuiServicosContratadosQueImpedeNovaContratacao)
            return resultadoValidacaoCredito;

        resultadoValidacaoCredito = StatusCredito.Aprovado;

        return resultadoValidacaoCredito;
    }

    #region Calculos
    internal async Task<ConsultarCreditoQueryResponse?> ObterCalculoConsignado(ConsultarCreditoQuery request, CancellationToken cancellationToken)
    {
        const double taxaJurosCreditoConsignado = 1;

        var obterCalculoCreditoConsignadoRequest = new ObterCalculoCreditoConsignadoRequest
        {
            DataPrimeiroPagamento = request.DataPrimeiroVencimento,
            QuantidadeParcelas = request.QuantidadeParcelas,
            TaxaJuros = taxaJurosCreditoConsignado,
            ValorCreditoSolicitado = request.ValorCredito
        };

        var result = await _calculationManagerService.ObterCalculoConsignado(obterCalculoCreditoConsignadoRequest, cancellationToken);

        return result is null ? default : new ConsultarCreditoQueryResponse(result.ValorCalculo, result.ValorCalculo - request.ValorCredito);
    }

    internal async Task<ConsultarCreditoQueryResponse?> ObterCalculoDireto(ConsultarCreditoQuery request, CancellationToken cancellationToken)
    {
        const double taxaJurosCreditoDireto = 2;

        var obterCalculoCreditoDiretoRequest = new ObterCalculoCreditoDiretoRequest
        {
            DataPrimeiroPagamento = request.DataPrimeiroVencimento,
            QuantidadeParcelas = request.QuantidadeParcelas,
            TaxaJuros = taxaJurosCreditoDireto,
            ValorCreditoSolicitado = request.ValorCredito
        };

        var result = await _calculationManagerService.ObterCalculoDireto(obterCalculoCreditoDiretoRequest, cancellationToken);

        return result is null ? default : new ConsultarCreditoQueryResponse(result.ValorCalculo, result.ValorCalculo - request.ValorCredito);
    }

    internal async Task<ConsultarCreditoQueryResponse?> ObterCalculoEmprestimoPessoaFisica(ConsultarCreditoQuery request, CancellationToken cancellationToken)
    {
        const double taxaJurosCreditoPessoaFisica = 3;

        var obterCalculoCalculoEmprestimoPessoaFisicaRequest = new ObterCalculoCreditoEmprestimoPessoaFisicaRequest
        {
            DataPrimeiroPagamento = request.DataPrimeiroVencimento,
            QuantidadeParcelas = request.QuantidadeParcelas,
            TaxaJuros = taxaJurosCreditoPessoaFisica,
            ValorCreditoSolicitado = request.ValorCredito
        };

        var result = await _calculationManagerService.ObterCalculoEmprestimoPessoaFisica(obterCalculoCalculoEmprestimoPessoaFisicaRequest, cancellationToken);

        return result is null ? default : new ConsultarCreditoQueryResponse(result.ValorCalculo, result.ValorCalculo - request.ValorCredito);
    }

    internal async Task<ConsultarCreditoQueryResponse?> ObterCalculoEmprestimoPessoaJuridica(ConsultarCreditoQuery request, CancellationToken cancellationToken)
    {
        const double taxaJurosCreditoPessoaJuridica = 5;

        var obterCalculoCreditoEmprestimoPessoaJuridicaRequest = new ObterCalculoCreditoEmprestimoPessoaJuridicaRequest
        {
            DataPrimeiroPagamento = request.DataPrimeiroVencimento,
            QuantidadeParcelas = request.QuantidadeParcelas,
            TaxaJuros = taxaJurosCreditoPessoaJuridica,
            ValorCreditoSolicitado = request.ValorCredito
        };

        var result = await _calculationManagerService.ObterCalculoEmprestimoPessoaJuridica(obterCalculoCreditoEmprestimoPessoaJuridicaRequest, cancellationToken);

        return result is null ? default : new ConsultarCreditoQueryResponse(result.ValorCalculo, result.ValorCalculo - request.ValorCredito);
    }

    internal async Task<ConsultarCreditoQueryResponse?> ObterCalculoImobiliario(ConsultarCreditoQuery request, CancellationToken cancellationToken)
    {
        const double taxaJurosCreditoImobiliario = 9;

        var obterCalculoCreditoImobiliarioRequest = new ObterCalculoCreditoImobiliarioRequest
        {
            DataPrimeiroPagamento = request.DataPrimeiroVencimento,
            QuantidadeParcelas = request.QuantidadeParcelas,
            TaxaJuros = taxaJurosCreditoImobiliario,
            ValorCreditoSolicitado = request.ValorCredito
        };

        var result = await _calculationManagerService.ObterCalculoImobiliario(obterCalculoCreditoImobiliarioRequest, cancellationToken);

        return result is null ? default : new ConsultarCreditoQueryResponse(result.ValorCalculo, result.ValorCalculo - request.ValorCredito);
    }
    #endregion
}