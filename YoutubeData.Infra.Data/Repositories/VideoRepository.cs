using YoutubeData.Domain.Entities;
using YoutubeData.Domain.Interfaces.Repositories;
using YoutubeData.Infra.Data.Contexts;
using YoutubeData.Infra.Data.Repositories.Base;

namespace YoutubeData.Infra.Data.Repositories;

public class VideoRepository : Repository<YoutubeDataContext, Video>, IVideoRepository
{
    public VideoRepository(YoutubeDataContext database)
        : base(database)
    {
    }
}
