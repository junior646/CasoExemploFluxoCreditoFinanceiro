using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Common.Interfaces;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
