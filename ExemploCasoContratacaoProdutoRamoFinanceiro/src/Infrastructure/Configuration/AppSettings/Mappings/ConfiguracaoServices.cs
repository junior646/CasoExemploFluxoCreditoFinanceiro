namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Configuration.AppSettings.Mappings;
internal class ConfiguracaoServices
{
    public MotorCalculoServiceSettings MotorCalculoService { get; set; }
    public ValidacaoClienteServiceSettings ValidacaoClienteService { get; set; }
    
    public struct MotorCalculoServiceSettings
    {
        public string UriBase { get; set; }
    }

    public struct ValidacaoClienteServiceSettings
    {
        public string UriBase { get; set; }
    }
}