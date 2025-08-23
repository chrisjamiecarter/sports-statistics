using System;
using System.Diagnostics;
using SportsStatistics.Application.Interfaces;

namespace SportsStatistics.Tools.DatabaseMigrator;

public class Worker : BackgroundService
{
    public const string ActivitySourceName = "Migrations";

    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);
    
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(IHostApplicationLifetime hostApplicationLifetime , ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = ActivitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var migrationService = scope.ServiceProvider.GetRequiredService<IDatabaseMigrationService>();

            var result = await migrationService.MigrateAsync(stoppingToken);

            if (result.IsSuccess)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Database migration result: {Result}", result);
                }
            }
            else
            {
                throw result.Exception ?? new InvalidOperationException(result.Message);
            }
        }
        catch (Exception exception)
        {
            activity?.AddException(exception);
            throw;
        }

        _hostApplicationLifetime.StopApplication();
    }
}
