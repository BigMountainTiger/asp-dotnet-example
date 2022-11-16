using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var section = builder.Configuration.GetSection("Example");
var exampleConfiguration = section.Get<ExampleConfiguration>();
if (exampleConfiguration == null)
    throw new ApplicationException("Unable to get the example section");

builder.Services.Configure<ExampleConfiguration>(section);

var CORS_CONFIG = "CORS_CONFIG";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: CORS_CONFIG,
        x => {
            x.AllowAnyHeader();
            x.AllowAnyMethod();
            x.AllowAnyOrigin();
        }
    );
});

var app = builder.Build();

app.Use(async (ctx, next) =>
{
    var headers = ctx.Response.Headers;
    headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    headers["Pragma"] = "no-cache";
    headers["Expires"] = "-1";

    await next.Invoke();
});

app.UseCors(CORS_CONFIG);


app.MapGet("/", async (IOptions<ExampleConfiguration> config) =>
{

    var result = await Task.Run(() => config.Value.Text);
    return $"{result}";
});

app.Run("http://*:8080");
