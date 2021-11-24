using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YoutubeData.Domain.Entities;
using YoutubeData.Infra.Data.Mappings;

namespace YoutubeData.Infra.Data.Contexts;

public class YoutubeDataContext : DbContext, IDatabaseContext
{
    // Para rodar a migration de criação do banco, descomente
    //public YoutubeDataContext()
    //{

    //}

    // Para rodar a migration de criação do banco, comente
    public YoutubeDataContext(DbContextOptions<YoutubeDataContext> options)
        : base(options)
    {
    }

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
            var stringConnection = configuration.GetConnectionString("DefaultConnection");
            var mySqlMajorVersion = Convert.ToInt32(configuration["MySql:Major"]);
            var mySqlMinorVersion = Convert.ToInt32(configuration["MySql:Minor"]);
            var mySqlBuildVersion = Convert.ToInt32(configuration["MySql:Build"]);
            var serverVersion = new MySqlServerVersion(new Version(mySqlMajorVersion, mySqlMinorVersion, mySqlBuildVersion));
            optionsBuilder.UseMySql(stringConnection, serverVersion);
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
