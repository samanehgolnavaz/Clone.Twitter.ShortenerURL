


namespace Clone.Twitter.ShortenerUrl.Persistence
{
    public class ShortenerDbContext :DbContext
    {

        public const string ConnectionStringName = "Database";
        public ShortenerDbContext(DbContextOptions<ShortenerDbContext> dbContextOptions)
            :base(dbContextOptions) 
        {

        }

        //public DbSet<Tag> Tags { get; set; }
        public DbSet<Tag> Tags => Set<Tag>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tag>(tag =>
            {
                tag.Property(s => s.ShortenCode)
                   .HasMaxLength(20)
                   .IsFixedLength(false)               
                   .IsRequired(true);

                tag.Property(s => s.LongUrl)
                   .HasMaxLength(2039)
                   .IsRequired(true);

                tag.HasKey(s => s.Id);
                tag.ToTable($"{nameof(Tag)}s");

                tag.HasIndex(s => s.ShortenCode)
                   .IsUnique()
                   .IsClustered(false);

            });

          
        }
    }
}
