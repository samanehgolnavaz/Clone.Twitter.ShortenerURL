namespace Clone.Twitter.ShortenerUrl.Persistence
{
    public interface ITagRepository
    {
        Task<Tag> InsertAsync(Tag tag,CancellationToken cancellationToken=default);
        Task<Tag?> FindByShortenCodeAsync(string shortenCode, CancellationToken cancellationToken=default);
        Task<Tag?> FindByLongUrlAsync(string longUrl, CancellationToken cancellationToken = default);

    }
}
