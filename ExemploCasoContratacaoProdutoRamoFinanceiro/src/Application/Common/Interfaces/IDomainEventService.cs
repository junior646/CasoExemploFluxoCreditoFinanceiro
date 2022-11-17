using ExemploCasoContratacaoProdutoRamoFinanceiro.Domain.Common;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Common.Interfaces;
public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
