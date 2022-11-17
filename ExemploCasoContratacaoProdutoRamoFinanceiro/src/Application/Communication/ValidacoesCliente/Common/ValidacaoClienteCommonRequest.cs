namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.ValidacoesCliente.Common;
public abstract class ValidacaoClienteCommonRequest
{
    public long CPF { get; set; }
    public long CNPJ { get; set; }
}