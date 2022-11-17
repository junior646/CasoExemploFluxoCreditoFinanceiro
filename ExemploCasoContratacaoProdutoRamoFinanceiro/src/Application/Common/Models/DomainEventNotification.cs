using ExemploCasoContratacaoProdutoRamoFinanceiro.Domain.Common;
using MediatR;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.Common.Models;
public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
{
    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}
