using ExemploCasoContratacaoProdutoRamoFinanceiro.Domain.Enums;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.UseCases.ContratacaoCredito.Queries.ConsultarCredito;

public record ConsultarCreditoQueryResponse(double ValorTotalComJuros = 0, double ValorJuros = 0)
{
    public StatusCredito StatusCredito { get; private set; } = StatusCredito.Recusado;
    internal void PreencherStatusCredito(StatusCredito permitirContratacaoCredito)
    {
        StatusCredito = permitirContratacaoCredito;
    }
}