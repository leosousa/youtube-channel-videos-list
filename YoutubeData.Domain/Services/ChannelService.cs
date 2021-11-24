using YoutubeData.Domain.Entities;
using YoutubeData.Domain.Interfaces.Repositories;
using YoutubeData.Domain.Interfaces.Services;
using YoutubeData.Domain.Services.Base;

namespace YoutubeData.Domain.Services;

public class ChannelService : DomainService<Channel>, IChannelService
{
    public ChannelService(IChannelRepository repository)
            : base(repository)
    {
    }
}
