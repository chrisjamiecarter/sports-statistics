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
            .Select(t => new
            {
                Implementation = t,
                Interfaces = t.GetInterfaces()
                .Where(i => i.IsGenericType &&
                    (
                        i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                        i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>) ||
                        i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)
                    ))
            })
            .Where(x => x.Interfaces.Any());

        foreach (var handler in handlers)
        {
            foreach (var i in handler.Interfaces)
            {
                services.AddScoped(i, handler.Implementation);

                // If it's ICommandHandler or IQueryHandler, also register IRequestHandler
                var definition = i.GetGenericTypeDefinition();
                if (definition == typeof(ICommandHandler<,>) || definition == typeof(IQueryHandler<,>))
                {
                    var args = i.GetGenericArguments();
                    var requestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(args);
                    services.AddScoped(requestHandlerType, handler.Implementation);
                }
            }
        }

        return services;
    }
}
