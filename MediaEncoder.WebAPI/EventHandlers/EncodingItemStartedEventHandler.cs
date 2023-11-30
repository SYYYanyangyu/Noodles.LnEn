using MediaEncoder.Domain.Events;
using MediatR;
using Noodles.EventBus;

namespace MediaEncoder.WebAPI.EventHandlers;

// 千万别掉了 不然无法响应集成事件
public class EncodingItemStartedEventHandler : INotificationHandler<EncodingItemStartedEvent>
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