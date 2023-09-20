using SalesAndPurchase.Server.Api;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host
    .UseSerilog((builderContext, config) =>
    {
        config
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.Http("http://logstash:5000", queueLimitBytes: null);
    });


var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

await app.RunAsync();