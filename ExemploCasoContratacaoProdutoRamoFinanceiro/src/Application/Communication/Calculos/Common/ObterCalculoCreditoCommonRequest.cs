namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.Common;
public abstract class ObterCalculoCreditoCommonRequest
{
    public double ValorCreditoSolicitado { get; set; }
    public int QuantidadeParcelas { get; set; }
    public double TaxaJuros { get; set; }
    public DateTime DataPrimeiroPagamento { get; set; }
}