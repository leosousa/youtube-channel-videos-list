namespace YoutubeData.Domain.Entities.Base;

/// <summary>
/// Entidade base para compartilhar dados comuns a todas as entidades
/// </summary>
public class Entity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}