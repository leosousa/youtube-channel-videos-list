using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YoutubeData.Domain.Entities;
using YoutubeData.Infra.Data.Mappings;

namespace YoutubeData.Infra.Data.Contexts;

public class YoutubeDataContext : DbContext, IDatabaseContext
{
    // Para rodar a migration de criação do banco, comente
    public YoutubeDataContext(DbContextOptions<YoutubeDataContext> options)
        : base(options)
    {
    }

    //// Para rodar a migration de criação do banco, descomente
    //public YoutubeDataContext()
    //{

    //}

    public DbSet<Channel>? Channels { get; set; }
    public DbSet<Video>? Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ChannelMap());
        modelBuilder.ApplyConfiguration(new VideoMap());
    }

    public override int SaveChanges()
    {
        SetupDateRegisterOnlyAdd("CreatedAt");
        SetupDateRegisterOnlyUpdate("UpdatedAt");
        return base.SaveChanges();
    }

    protected virtual void SetupDateRegisterOnlyAdd(string nameDateField)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(nameDateField) != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameDateField).CurrentValue = DateTime.Now;
            }
        }
    }

    protected virtual void SetupDateRegisterOnlyUpdate(string nameDateField)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(nameDateField) != null))
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Property(nameDateField).CurrentValue = DateTime.Now;
            }
        }
    }
}
