using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoutubeData.Domain.Entities;

namespace YoutubeData.Infra.Data.Mappings;

public class VideoMap : IEntityTypeConfiguration<Video>
{
	public void Configure(EntityTypeBuilder<Video> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Title).HasMaxLength(100).IsRequired(true);
		builder.Property(c => c.UrlVideo).HasMaxLength(100).IsRequired(true);
		builder.Property(c => c.UrlThumbnail).HasMaxLength(100).IsRequired(true);
		builder.Property(c => c.CreatedAt).IsRequired(true);
		builder.Property(c => c.UpdatedAt).IsRequired(true);

		builder.ToTable("Video");
	}
}
