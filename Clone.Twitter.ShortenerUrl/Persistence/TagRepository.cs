using System.Runtime.CompilerServices;

namespace Clone.Twitter.ShortenerUrl.Persistence
{
    public class TagRepository : ITagRepository
    {
        private readonly ShortenerDbContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public TagRepository(ShortenerDbContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }
        public async Task<Tag> InsertAsync(Tag tag, CancellationToken cancellationToken = default)
        {
            var createdTag = (await _dbContext.Tags.AddAsync(tag, cancellationToken)).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken);
            _memoryCache.Set(tag.ShortenCode, createdTag);
            return createdTag;

        }

        public async Task<Tag?> FindByLongUrlAsync(string longUrl, CancellationToken cancellationToken = default)
        => await _dbContext.Tags.FirstOrDefaultAsync(x => x.LongUrl == longUrl);

        public async Task<Tag?> FindByShortenCodeAsync(string shortenCode, CancellationToken cancellationToken = default)
        {
            if(_memoryCache.TryGetValue<Tag>(shortenCode, out var tag))
            {
                return tag;
            }
           var findTag= await _dbContext.Tags.FirstOrDefaultAsync(x => x.ShortenCode == shortenCode);
            if (findTag == null) return null;
            _memoryCache.Set(tag.ShortenCode, findTag);
            return findTag;

        }
    }
}
