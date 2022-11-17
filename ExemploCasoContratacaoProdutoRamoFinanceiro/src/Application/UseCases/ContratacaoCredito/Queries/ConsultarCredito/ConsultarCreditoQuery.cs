﻿using MediatR;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Domain.Enums;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.UseCases.ContratacaoCredito.Queries.ConsultarCredito;

public record ConsultarCreditoQuery(TipoCredito TipoCredito, double ValorCredito, int QuantidadeParcelas, DateTime DataPrimeiroVencimento) : IRequest<ConsultarCreditoQueryResponse>;