using MediatR;

namespace Application.Abstraction.Messaging;

public interface IEvent : INotification
{
    public Guid Id { get; init; }
}