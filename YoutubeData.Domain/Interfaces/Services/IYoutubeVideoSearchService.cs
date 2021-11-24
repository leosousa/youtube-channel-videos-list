using YoutubeData.Domain.Entities;

namespace YoutubeData.Domain.Interfaces.Services;

public interface IYoutubeVideoSearchService
{
    Task<IEnumerable<Video>?> SearchNewVideos();
}
