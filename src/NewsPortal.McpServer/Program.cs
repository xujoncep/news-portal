using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;
using NewsPortal.Application;
using NewsPortal.Infrastructure;
using NewsPortal.McpServer.Tools;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("logs/mcp-server-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting NewsPortal MCP Server");

    var builder = Host.CreateApplicationBuilder(args);

    // Add configuration
    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true)
        .AddEnvironmentVariables();

    // Add services
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    // Add MCP Server
    builder.Services.AddMcpServer()
        .WithStdioServerTransport()
        .WithToolsFromAssembly(typeof(NewsTools).Assembly);

    builder.Services.AddLogging(logging =>
    {
        logging.AddSerilog(dispose: true);
    });

    var host = builder.Build();
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
