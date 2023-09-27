namespace Clone.Twitter.ShortenerUrl.Settings;

public class ShortenSetting
{
    public const string SectionName = "Shorten";
    public required string BaseUrl { get; set; }
    public int GeneratedCodeLength { get; set; }
    public int ExpirationInDays { get; set; }
}
