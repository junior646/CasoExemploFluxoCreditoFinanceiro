namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.ValidacoesCliente.ConsultarBloqueiosCliente;

public class ConsultarBloqueiosClienteResponse
{
    public bool PossuiBloqueios { get; set; }
    public string[] BloqueiosEncontrados { get; set; } = Array.Empty<string>();
}