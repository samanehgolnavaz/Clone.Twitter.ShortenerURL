var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<ShortenerDbContext>(configure =>
{
    configure.UseSqlServer(builder.Configuration.GetConnectionString(ShortenerDbContext.ConnectionStringName));
});
var app = builder.Build();
builder.Services.Configure<ShortenSetting>(builder.Configuration.GetSection(ShortenSetting.SectionName));
builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
app.MapPost("/Shorten", (
    string url,
    IUrlShortenerService service)
    => service.GetShortenUrlAsync(url));

app.MapGet("{shortenCode}", async (
    [FromRoute] string shortenCode,
    IUrlShortenerService service) =>
{
    var longUrl=await service.GetLognUrlAsync(shortenCode); 
    if(string.IsNullOrEmpty(longUrl))
    {
        return Results.NotFound();
    }
    return Results.Redirect(longUrl);
});
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
