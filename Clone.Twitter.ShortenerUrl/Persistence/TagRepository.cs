using System.Runtime.CompilerServices;

namespace Clone.Twitter.ShortenerUrl.Persistence
{
    public class TagRepository : ITagRepository
    {
        private readonly ShortenerDbContext _dbContext;
        public TagRepository(ShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Tag> InsertAsync(Tag tag, CancellationToken cancellationToken = default)
        {
            var createdTag = (await _dbContext.Tags.AddAsync(tag, cancellationToken)).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return createdTag;

        }

        public async Task<Tag?> FindByLongUrlAsync(string longUrl, CancellationToken cancellationToken = default)
        => await _dbContext.Tags.FirstOrDefaultAsync(x => x.LongUrl == longUrl);

        public async  Task<Tag?> FindByShortenCodeAsync(string shortenCode, CancellationToken cancellationToken = default)
       => await _dbContext.Tags.FirstOrDefaultAsync(x => x.ShortenCode==shortenCode);
    }
}
