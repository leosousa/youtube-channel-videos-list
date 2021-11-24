using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YoutubeData.Application.Interfaces;
using YoutubeData.Application.Services;
using YoutubeData.Domain.Interfaces.Repositories;
using YoutubeData.Domain.Interfaces.Services;
using YoutubeData.Domain.Services;
using YoutubeData.Infra.Data.Contexts;
using YoutubeData.Infra.Data.Repositories;

Console.WriteLine("****************************************");
Console.WriteLine("Robô de busca de novos vídeos no Youtube");
Console.WriteLine("****************************************");
Console.WriteLine("Iniciando...");
Console.WriteLine("Carregando configurações...");
// Instanciando configuração
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

Console.WriteLine("Carregando conexão com o banco de dados...");
var conexao = configuration.GetConnectionString("DefaultConnection");

var stringConnection = configuration.GetConnectionString("DefaultConnection");
var mySqlMajorVersion = Convert.ToInt32(configuration["MySql:Major"]);
var mySqlMinorVersion = Convert.ToInt32(configuration["MySql:Minor"]);
var mySqlBuildVersion = Convert.ToInt32(configuration["MySql:Build"]);
var serverVersion = new MySqlServerVersion(new Version(mySqlMajorVersion, mySqlMinorVersion, mySqlBuildVersion));

Console.WriteLine("Carregando infraestrutura...");
var serviceProvider = new ServiceCollection()
    .AddSingleton(provider => configuration)
    .AddDbContextPool<YoutubeDataContext>(
        dbContextOptions => dbContextOptions
            .UseMySql(conexao, serverVersion)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
     )
    .AddSingleton(typeof(IDatabaseContext), typeof(YoutubeDataContext))
    .AddSingleton<IChannelVideoSearchAppService, ChannelVideoSearchAppService>()
    .AddSingleton<IYoutubeVideoSearchService, YoutubeVideoSearchService>()
    .AddSingleton<IChannelService, ChannelService>()
    .AddSingleton<IChannelRepository, ChannelRepository>()
    .AddSingleton<IVideoService, VideoService>()
    .AddSingleton<IVideoRepository, VideoRepository>()
    .BuildServiceProvider();

// Obtendo instância
Console.WriteLine("Instanciando buscador de vídeos...");
var buscadorService = serviceProvider.GetService<IYoutubeVideoSearchService>();
if (buscadorService == null)
{
    Console.WriteLine("Buscador de vídeos não instanciado.");
    Console.WriteLine("Finalizando robô");
    return;
}

IChannelVideoSearchAppService _buscadorVideos = new ChannelVideoSearchAppService(buscadorService);
var result = buscadorService.SearchNewVideos().Result;