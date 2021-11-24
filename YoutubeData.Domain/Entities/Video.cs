using YoutubeData.Domain.Entities.Base;

namespace YoutubeData.Domain.Entities;

public class Video : Entity
{
    public Video(string title, string description, string urlVideo, string urlThumbnail, long idChannel, Channel channel)
    {
        Title = title;
        Description = description;
        UrlVideo = urlVideo;
        UrlThumbnail = urlThumbnail;
        IdChannel = idChannel;
        Channel = channel;
    }

    /// <summary>
    /// Título do vídeo
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Descrição do vídeo
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Url do vídeo
    /// </summary>
    public string UrlVideo { get; set; }

    /// <summary>
    /// Url do thumbnail
    /// </summary>
    public string UrlThumbnail { get; set; }

    /// <summary>
    /// Referência do canal ao qual o vídeo pertence
    /// </summary>
    public long IdChannel { get; set; }
    public virtual Channel Channel { get; set; }
}
