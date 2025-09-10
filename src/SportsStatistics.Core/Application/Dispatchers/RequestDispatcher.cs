using Microsoft.Extensions.DependencyInjection;
using SportsStatistics.Core.Application.Abstractions;

namespace SportsStatistics.Core.Application.Dispatchers;

public class RequestDispatcher(IServiceProvider provider) : IRequestDispatcher
{
    private readonly IServiceProvider _provider = provider;

    public Task DispatchAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest
    {
        var handler = _provider.GetRequiredService<IRequestHandler<TRequest>>();
        return handler.HandleAsync(request, cancellationToken);
    }

    public Task<TResponse> DispatchAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        var handler = _provider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        return handler.HandleAsync(request, cancellationToken);
    }
}
