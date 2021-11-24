using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using YoutubeData.Domain.Entities;
using YoutubeData.Domain.Interfaces.Services;

namespace YoutubeData.Domain.Services;

public class YoutubeVideoSearchService : IYoutubeVideoSearchService
{
    private readonly IChannelService _channelService;
    private readonly IVideoService _videoService;
    private readonly IConfiguration _configuration;

    public YoutubeVideoSearchService(IChannelService channelService, IVideoService videoService, IConfiguration configuration)
    {
        _channelService = channelService;
        _videoService = videoService;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Video>?> SearchNewVideos()
    {
        // Obtendo todos os canais cadastrados
        Console.WriteLine("Buscando canais cadastrados");
        var channelList = await _channelService.List();

        // Verifica se tem canais cadastrados
        if (channelList == null || !channelList.Any())
        {
            Console.WriteLine("Nenhum canal encontrado");
            return null;
        }

        var listaVideos = new List<Video>();

        // Varre lista de canais
        Console.WriteLine("Encontrado {0} canais", channelList.Count());
        int videoTotalCount = 0;
        foreach (var channel in channelList)
        {
            try
            {
                // Inicia contador de vídeos do canal
                var videoChannelCount = 0;

                // Instancia serviço do Youtube
                var youtubeService = new YouTubeService(new BaseClientService.Initializer() { ApiKey = _configuration["Youtube:ApiKey"] });

                // Obtém o id do canal
                var channelId = channel.Url.Replace("https://www.youtube.com/channel/", "");

                // Obtém os atributos que se deja obter
                var channelsListRequest = youtubeService.Channels.List("contentDetails");
                channelsListRequest.Id = channelId;

                // Executa requisição de busca de dados
                var channelsListResponse = channelsListRequest.Execute();

                // Para cada canal encontrado...
                foreach (var youtubeChannel in channelsListResponse.Items)
                {
                    // Obtém a playslist relacionada
                    var uploadsListId = youtubeChannel.ContentDetails.RelatedPlaylists.Uploads;

                    // De forma gradual, vai buscando os itens da playlist do canal na API 
                    // do Youtube de acordo com a quantidade configurada (50 em 50)
                    var nextPageToken = "";
                    while (nextPageToken != null)
                    {
                        // Obtém fragmentos da playlist
                        var playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                        playlistItemsListRequest.PlaylistId = uploadsListId;
                        playlistItemsListRequest.MaxResults = 50;
                        playlistItemsListRequest.PageToken = nextPageToken;

                        // Recupera a lista de vídeos enviada para o canal  
                        var playlistItemsListResponse = playlistItemsListRequest.Execute();
                        foreach (var playlistItem in playlistItemsListResponse.Items)
                        {
                            // Adiciona na lista de vídeos
                            listaVideos.Add(new Video(
                                title: playlistItem.Snippet.Title,
                                description: playlistItem.Snippet.Description,
                                urlVideo: _configuration["Youtube:UrlBaseVideo"] + playlistItem.Snippet.ResourceId.VideoId,
                                urlThumbnail: playlistItem.Snippet.Thumbnails.High.Url,
                                idChannel: channel.Id,
                                channel: channel
                            ));

                            // Atualiza o contador de vídeos geral
                            videoTotalCount++;

                            // Atualiza o contador de vídeos do canal
                            videoChannelCount++;
                        }
                        nextPageToken = playlistItemsListResponse.NextPageToken;
                    }
                }

                if (videoChannelCount > 0)
                {
                    Console.WriteLine("Encontrado {0} vídeos no canal {1}", videoChannelCount, channel.Name);
                }
                else
                {
                    Console.WriteLine("Nenhum vídeo encontrado para o canal {1}", channel.Name);
                }

                Console.WriteLine("Encontrado um total geral de {0} vídeos", videoTotalCount);

            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao buscar vídeos. Detalhes: " + e.Message);
            }
        }

        // Obtendo lista de vídeos do banco
        var videosCadastrados = await _videoService.List();

        if (listaVideos == null || !listaVideos.Any())
        {
            Console.WriteLine("Nenhum vídeo novo encontrado");
            return null;
        }

        var contadorVideosNovos = 0;

        // Processa os vídeos encontrados
        foreach (var video in listaVideos)
        {
            // Verifica se o vídeo já não existe na lista
            if (!videosCadastrados.Any(e => e.UrlVideo == video.UrlVideo))
            {
                // Caso não exista, adiciona
                _videoService.Create(video);
                contadorVideosNovos++;
                Console.WriteLine("Novo vídeo encontrado no canal {0}: {1}", video.Channel.Name, video.UrlVideo);
            }
        }

        Console.WriteLine("Total de {0} novos vídeos encontrados", contadorVideosNovos);

        return await Task.FromResult(listaVideos);
    }
}
