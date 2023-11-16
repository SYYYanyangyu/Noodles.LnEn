using MediaEncoder.Domain.Events;
using Noodles.EventBus;

namespace MediaEncoder.WebAPI.EventHandlers;

public class EncodingItemFailedEventHandler
{
    private readonly IEventBus eventBus;

    public EncodingItemFailedEventHandler(IEventBus eventBus)
    {
        this.eventBus = eventBus;
    }
    public Task Handle(EncodingItemFailedEvent notification, CancellationToken cancellationToken)
    {
        eventBus.Publish("MediaEncoding.Failed", notification);
        return Task.CompletedTask;
    }
}