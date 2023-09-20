﻿using SalesAndPurchase.Server.Application.EventBus.EventHandlings;
using SalesAndPurchase.Server.Application.EventBus.Events;

namespace SalesAndPurchase.Server.Application.EventBus
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }
}
