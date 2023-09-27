namespace Clone.Twitter.ShortenerUrl.Models.Entities
{
    public class Tag
    {
        public long Id { get; set; }
        public required string ShortenCode { get; set; }
        public required string LongUrl { get; set; }
        public DateTime ExpirationOn { get; set; }
        public static Tag Create(
            string shortenCode,
            string longUrl,
            DateTime expirationOn)
            => new Tag
            {
                LongUrl = longUrl,
                ShortenCode = shortenCode,
                ExpirationOn = expirationOn

            };

    }
}
