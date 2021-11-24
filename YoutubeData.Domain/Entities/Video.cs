using YoutubeData.Domain.Entities.Base;

namespace YoutubeData.Domain.Entities;

public class Video : Entity
{
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
    public string Url { get; set; }

    /// <summary>
    /// Url do thumbnail
    /// </summary>
    public string UrlThumbnail { get; set; }

    /// <summary>
    /// Referência do canal ao qual o vídeo pertence
    /// </summary>
    public int IdChannel { get; set; }
    public virtual Channel Channel { get; set; }
}
