using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStatistics.Core.Application.Abstractions;
using SportsStatistics.Core.Application.Dispatchers;

namespace SportsStatistics.Application;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApplicationDependencies(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.Services.AddCqrsServices();

        return builder;
    }

    public static IServiceCollection AddCqrsServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddScoped<IRequestDispatcher, RequestDispatcher>();
        services.AddCqrsHandlers(AssemblyReference.Assembly);

        return services;
    }

    private static IServiceCollection AddCqrsHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        var handlers = assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
            .Where(ti => ti.Interface.IsGenericType &&
                         (ti.Interface.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                          ti.Interface.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                          ti.Interface.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
            .ToList();

        foreach (var handler in handlers)
        {
            services.AddScoped(handler.Interface, handler.Type);
        }

        return services;
    }
}
