using FluentValidation;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Domain.Enums;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.UseCases.ContratacaoCredito.Queries.ConsultarCredito;
public class ConsultarCreditoValidator : AbstractValidator<ConsultarCreditoQuery>
{
    const int QuantidadeMinimaParcelas = 5;
    const int QuantidadeMaximaParcelas = 72;
    const double ValorMaximoContratacao = 1000000.00;
    const double ValorMinimoContratacaoJuridico = 15000.00;
    const int QuantidadeMinimaDiasPrimeiroVencimento = 15;
    const int QuantidadeMaximaDiasPrimeiroVencimento = 40;

    public ConsultarCreditoValidator()
    {
        RuleFor(v => v.TipoCredito)
            .NotEmpty()
            .IsInEnum();

        RuleFor(v => v.DataPrimeiroVencimento)
            .NotEmpty()
            .GreaterThan(DateTime.Now.AddDays(QuantidadeMinimaDiasPrimeiroVencimento))
            .LessThan(DateTime.Now.AddDays(QuantidadeMaximaDiasPrimeiroVencimento));

        RuleFor(v => v)
            .NotNull()
            .Must(v => ValidarValorCredito(v.TipoCredito, v.ValorCredito))
            .WithMessage(v => $"Valor de Crédito deve ser entre R$ {(v.TipoCredito is TipoCredito.PessoaJuridica ? ValorMinimoContratacaoJuridico : 0)},00 e R$ {ValorMaximoContratacao},00.");

        RuleFor(v => v.QuantidadeParcelas)
            .NotEmpty()
            .GreaterThan(QuantidadeMinimaParcelas)
            .LessThan(QuantidadeMaximaParcelas);

    }

    internal static bool ValidarValorCredito(TipoCredito tipoCredito, double valorCredito) =>
        tipoCredito is TipoCredito.PessoaJuridica
            ? valorCredito > ValorMinimoContratacaoJuridico && valorCredito < ValorMaximoContratacao
            : valorCredito < ValorMaximoContratacao;
}