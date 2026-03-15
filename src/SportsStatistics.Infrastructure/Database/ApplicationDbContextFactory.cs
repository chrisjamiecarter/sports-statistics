using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStatistics.Infrastructure.Database;

internal sealed class ApplicationDbContextFactory<TContext>(
    IServiceProvider provider)
    : IDbContextFactory<TContext>
    where TContext : DbContext
{
    private readonly IServiceProvider _provider = provider;

    public TContext CreateDbContext() => ActivatorUtilities.CreateInstance<TContext>(_provider);
}
