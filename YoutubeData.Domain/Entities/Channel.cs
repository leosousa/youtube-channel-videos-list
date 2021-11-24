using YoutubeData.Domain.Entities.Base;

namespace YoutubeData.Domain.Entities;

/// <summary>
/// Armazena informações de um canal do Youtube
/// </summary>
public class Channel : Entity
{
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
    public virtual IEnumerable<Video> Videos { get; set; }
}
