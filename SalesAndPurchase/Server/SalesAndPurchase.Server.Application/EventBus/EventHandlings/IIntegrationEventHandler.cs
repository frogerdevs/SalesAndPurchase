using SalesAndPurchase.Server.Application.EventBus.Events;

namespace SalesAndPurchase.Server.Application.EventBus.EventHandlings
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
     where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
