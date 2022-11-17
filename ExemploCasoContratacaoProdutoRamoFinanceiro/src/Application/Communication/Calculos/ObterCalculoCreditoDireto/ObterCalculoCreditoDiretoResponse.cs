namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.Calculos.ObterCalculoCreditoDireto;

public class ObterCalculoCreditoDiretoResponse
{
    public double ValorCalculo { get; set; }
    public double ValorPorParcela { get; set; }
    public DateTime DataPrimeiraParcela { get; set; }
    public double ValorPrimeiraParcela { get; set; }
    public DateTime DataUltimaParcela { get; set; }
    public double ValorUltimaParcela { get; set; }
}