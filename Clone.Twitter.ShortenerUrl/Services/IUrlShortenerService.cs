namespace Clone.Twitter.ShortenerUrl.Services;

public interface IUrlShortenerService
{
     Task<string> GetShortenUrlAsync(string longUrl,CancellationToken cancellationToken=default);
     Task<string?> GetLognUrlAsync(string shortenCode,CancellationToken cancellationToken=default);
   
}
