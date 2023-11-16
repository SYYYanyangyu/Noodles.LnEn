using MediaEncoder.Domain.Events;
using Noodles.EventBus;

namespace MediaEncoder.WebAPI.EventHandlers;

public class EncodingItemStartedEventHandler
{
    private readonly IEventBus eventBus;

    public EncodingItemStartedEventHandler(IEventBus eventBus)
    {
        this.eventBus = eventBus;
    }
    public Task Handle(EncodingItemStartedEvent notification, CancellationToken cancellationToken)
    {
        eventBus.Publish("MediaEncoding.Started", notification);
        return Task.CompletedTask;
    }
}