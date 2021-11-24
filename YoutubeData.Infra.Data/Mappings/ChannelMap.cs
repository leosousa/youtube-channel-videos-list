using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeData.Domain.Entities;

namespace YoutubeData.Infra.Data.Mappings;

public class ChannelMap : IEntityTypeConfiguration<Channel>
{
	public void Configure(EntityTypeBuilder<Channel> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Name).HasMaxLength(100).IsRequired(true);
		builder.Property(c => c.Description).HasMaxLength(200).IsRequired(true);
		builder.Property(c => c.Url).HasMaxLength(100).IsRequired(true);
		builder.Property(c => c.CreatedAt).IsRequired(true);
		builder.Property(c => c.UpdatedAt).IsRequired(true);

		builder.HasMany(s => s.Videos)
			  .WithOne(g => g.Channel)
			  .HasForeignKey(s => s.IdChannel);

		builder.ToTable("Channel");
	}
}
