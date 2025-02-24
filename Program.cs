using APIMeteo.Infrastructure.Database;
using APIMeteo.Infrastructure.Datalayers;
using APIMeteo.Interfaces.DataLayers;
using APIMeteo.Models;
using APIMeteo.Models.Internal;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

try
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerDocument(config =>
    {
        config.PostProcess = document =>
        {
            document.Info.Title = "API Documentation";
            document.Info.Description = "API Documentation for the APIMeteo";
            document.Info.Version = "v1";
        };
    });

    ConfigAPI configAPI = new ConfigAPI();

    IConfiguration _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    configAPI = _configuration.GetSection("ConfigAPI").Get<ConfigAPI>();

    builder.Services.AddScoped<IRoomDatalayer, RoomDatalayer>();
    builder.Services.AddScoped<IMeasuresDatalayer, MeasureDataLayer>();
    builder.Services.AddScoped<IAlertDatalayer, AlertDatalayer>();

    builder.Services.AddDbContextPool<EntityFrameworkDbContext>(options =>
    {
        options.UseNpgsql(configAPI.SqlConnexion);
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

    builder.Services.Configure<ConfigAPI>(x => x.SqlConnexion = configAPI.SqlConnexion);

    var app = builder.Build();

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    app.MapGet("/Alive", () => "I'm alive!");

    app.UseOpenApi();
    app.UseSwaggerUi();

    app.MapControllers();

    await app.RunAsync();

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}