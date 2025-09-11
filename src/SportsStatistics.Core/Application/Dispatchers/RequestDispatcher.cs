using Microsoft.Extensions.DependencyInjection;
using SportsStatistics.Core.Application.Abstractions;

namespace SportsStatistics.Core.Application.Dispatchers;

public class RequestDispatcher(IServiceProvider provider) : IRequestDispatcher
{
    private readonly IServiceProvider _provider = provider;

    //public Task DispatchAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
    //    where TRequest : IRequest
    //{
    //    var handler = _provider.GetRequiredService<IRequestHandler<TRequest>>();
    //    return handler.HandleAsync(request, cancellationToken);
    //}

    public Task<TResponse> DispatchAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var handler = _provider.GetRequiredService(handlerType);

        return ((dynamic)handler).HandleAsync((dynamic)request, cancellationToken);
    }

    public Task<TResponse> DispatchAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(typeof(TRequest), typeof(TResponse));
        dynamic handler = _provider.GetRequiredService(handlerType);
        
        return handler.HandleAsync(request, cancellationToken);
    }
}
