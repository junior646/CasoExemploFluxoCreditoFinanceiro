namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Communication.ValidacoesCliente.ConsultarServicosContratadosCliente;

public class ConsultarServicosContratadosClienteResponse
{
    public bool PossuiServicosContratados { get; set; }
    public bool PossuiServicosContratadosQueImpedeNovaContratacao { get; set; }
    public string[] ServicosContratados { get; set; } = Array.Empty<string>();
}