var builder = WebApplication.CreateBuilder(args);
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
    return Results.Redirect( await service.GetLognUrlAsync(shortenCode));
});
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
