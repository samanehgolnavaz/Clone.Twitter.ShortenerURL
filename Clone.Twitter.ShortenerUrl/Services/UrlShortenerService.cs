using Clone.Twitter.ShortenerUrl.Models.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Clone.Twitter.ShortenerUrl.Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly ShortenSetting _setting;
    private static  Dictionary<string,string> _shortenUrls = new Dictionary<string,string>();
    private ITagRepository _tagsRepository;
    public UrlShortenerService(IOptions<ShortenSetting> setting, ITagRepository tagsRepository)
    {
        _setting = setting.Value;
        _tagsRepository = tagsRepository;
    }
    public async Task<string?> GetLognUrlAsync(string shortenCode, CancellationToken cancellationToken)
    {
        var tag = await _tagsRepository.FindByShortenCodeAsync(shortenCode, cancellationToken);
        return tag?.LongUrl;
    }

    public async Task<string> GetShortenUrlAsync(string longUrl, CancellationToken cancellationToken = default)
    {
        var tag=await _tagsRepository.FindByLongUrlAsync(longUrl, cancellationToken);
        if (tag != null) return GenerateShorenUrl(tag.ShortenCode);
        var shortenCode = await GenerateUniqueCodeAsync(longUrl,cancellationToken) ;
        //use factory method
        var newTag= Tag.Create(shortenCode, longUrl, DateTime.UtcNow.AddDays(_setting.ExpirationInDays));
        await _tagsRepository.InsertAsync(newTag);

        return GenerateShorenUrl(shortenCode);
    }
    private string GenerateShorenUrl(string shortenCode)=> $"{_setting.BaseUrl}/{shortenCode}";
    private async Task<string> GenerateUniqueCodeAsync(string longUrl,CancellationToken cancellationToken=default)
    {
        string hashCode=GenerateCode(longUrl);
        string code;
        int startIndex = 0;
        do
        {
            code = hashCode.Substring(startIndex, _setting.GeneratedCodeLength);
            startIndex++;
        } while ((await _tagsRepository.FindByShortenCodeAsync(code, cancellationToken)) != null);
        return code;
    }
    private string GenerateCode(string url)
    {
        using (MD5 hasher = MD5.Create())
        {
            var hashBytes=hasher.ComputeHash(Encoding.UTF8.GetBytes(url));
            var hashString=BitConverter.ToString(hashBytes).Replace("-","").ToLowerInvariant();
            return hashString;
        }
    }




}
