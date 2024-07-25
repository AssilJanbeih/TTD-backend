using MediatR;

namespace Application.Abstraction.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent
{
}
