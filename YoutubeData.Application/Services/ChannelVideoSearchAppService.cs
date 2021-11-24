using YoutubeData.Application.Interfaces;
using YoutubeData.Domain.Interfaces.Services;

namespace YoutubeData.Application.Services;

public class ChannelVideoSearchAppService : IChannelVideoSearchAppService
{
    protected static Mutex mutex = null;

    private readonly IYoutubeVideoSearchService _videoSearchService;

    public ChannelVideoSearchAppService(IYoutubeVideoSearchService videoSearchService)
    {
        _videoSearchService = videoSearchService;
    }

    public void SearchNewVideos()
    {
        Console.WriteLine("Iniciando processamento...");

        ValidarServicoEmExecucao();

        _videoSearchService.SearchNewVideos();

        Console.WriteLine("Encerrando processamento.");
        Thread.Sleep(5000);
    }

    public virtual void ValidarServicoEmExecucao()
    {
        bool createdNew;

        mutex = new Mutex(true, "Robot", out createdNew);

        if (!createdNew)
        {
            var message = "Robot is already running!";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }
}
