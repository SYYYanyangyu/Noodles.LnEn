﻿
using Listening.Domain.Events;
using MediatR;
using Noodles.EventBus;

namespace Listening.Admin.WebAPI.EventHandlers;

/// <summary>
/// 1.INotificationHandler<EpisodeCreatedEvent> 先监听领域事件
/// 2.Episode . bulid 里面注册领域事件
/// 3.eventBus.Publish("ListeningEpisode.Created", 发布成一个领域事件
/// 4.在 searchServiceAPI里面 监听这个集成事件：[EventName("ListeningEpisode.Created")] 完成了把这个数     据写进es中
/// </summary>
public class EpisodeCreatedEventHandler : INotificationHandler<EpisodeCreatedEvent>
{
    private readonly IEventBus eventBus;

    public EpisodeCreatedEventHandler(IEventBus eventBus)
    {
        this.eventBus = eventBus;
    }

    public Task Handle(EpisodeCreatedEvent notification, CancellationToken cancellationToken)
    {
        //把领域事件转发为集成事件，让其他微服务听到

        //在领域事件处理中集中进行更新缓存等处理，而不是写到Controller中。因为项目中有可能不止一个地方操作领域对象，这样就就统一了操作。
        var episode = notification.Value;
        var sentences = episode.ParseSubtitle();
        eventBus.Publish("ListeningEpisode.Created", new { Id = episode.Id, episode.Name, Sentences = sentences, episode.AlbumId, episode.Subtitle, episode.SubtitleType });//发布集成事件，实现搜索索引、记录日志等功能
        return Task.CompletedTask;
    }
}
