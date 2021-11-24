using YoutubeData.Domain.Entities.Base;

namespace YoutubeData.Domain.Entities;

/// <summary>
/// Armazena informações de um canal do Youtube
/// </summary>
public class Channel : Entity
{
    protected Channel() { /* Used by EF */ }

    public Channel(string name, string description, string url)
    {
        Name = name;
        Description = description;
        Url = url;
    }

    /// <summary>
    /// Nome do canal
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descrição do canal
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Url do canal
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Lista de vídeos do canal
    /// </summary>
    public virtual IEnumerable<Video>? Videos { get; set; }
}
