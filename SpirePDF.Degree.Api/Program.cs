using Serilog;
using SpirePdf.Diploma.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);
LoggingConfiguration.ConfigureSerilog(builder);

Log.Information("Initialize...");

builder.Services.AddMvc();

var app = builder.Build();

app.MapControllers();
app.Run();