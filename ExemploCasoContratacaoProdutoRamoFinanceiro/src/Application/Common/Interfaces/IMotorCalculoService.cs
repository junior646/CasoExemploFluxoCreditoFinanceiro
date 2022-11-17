﻿using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoDireto;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoConsignado;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoImobiliario;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoEmprestimoPessoaFisica;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoEmprestimoPessoaJuridica;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Common.Interfaces;
public interface IMotorCalculoService
{
    Task<ObterCalculoCreditoConsignadoResponse?> ObterCalculoConsignado(ObterCalculoCreditoConsignadoRequest request, CancellationToken cancellationToken);
    Task<ObterCalculoCreditoDiretoResponse?> ObterCalculoDireto(ObterCalculoCreditoDiretoRequest request, CancellationToken cancellationToken);
    Task<ObterCalculoCreditoImobiliarioResponse?> ObterCalculoImobiliario(ObterCalculoCreditoImobiliarioRequest request, CancellationToken cancellationToken);
    Task<ObterCalculoCreditoEmprestimoPessoaFisicaResponse?> ObterCalculoEmprestimoPessoaFisica(ObterCalculoCreditoEmprestimoPessoaFisicaRequest request, CancellationToken cancellationToken);
    Task<ObterCalculoCreditoEmprestimoPessoaJuridicaResponse?> ObterCalculoEmprestimoPessoaJuridica(ObterCalculoCreditoEmprestimoPessoaJuridicaRequest request, CancellationToken cancellationToken);
}