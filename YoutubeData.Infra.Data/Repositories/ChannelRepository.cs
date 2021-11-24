using Microsoft.EntityFrameworkCore;
using YoutubeData.Domain.Entities;
using YoutubeData.Domain.Interfaces.Repositories;
using YoutubeData.Infra.Data.Contexts;
using YoutubeData.Infra.Data.Repositories.Base;

namespace YoutubeData.Infra.Data.Repositories;

public class ChannelRepository : Repository<YoutubeDataContext, Channel>, IChannelRepository
{
    public ChannelRepository(YoutubeDataContext database)
        : base(database)
    {
    }

    public override IEnumerable<Channel> List()
    {
        return _database.Set<Channel>().Include(e => e.Videos).ToList();
    }
}
