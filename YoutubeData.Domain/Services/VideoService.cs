using YoutubeData.Domain.Entities;
using YoutubeData.Domain.Interfaces.Repositories;
using YoutubeData.Domain.Interfaces.Services;
using YoutubeData.Domain.Services.Base;

namespace YoutubeData.Domain.Services;

public class VideoService : DomainService<Video>, IVideoService
{
    public VideoService(IVideoRepository repository)
           : base(repository)
    {
    }
}
