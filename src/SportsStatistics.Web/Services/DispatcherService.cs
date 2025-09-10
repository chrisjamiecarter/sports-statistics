using SportsStatistics.Core.Application.Abstractions;

namespace SportsStatistics.Web.Services;

internal interface IDispatcherService
{
    Task<TResponse> DispatchAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}

internal sealed class DispatcherService(IRequestDispatcher dispatcher) : IDispatcherService
{
    private readonly IRequestDispatcher _dispatcher = dispatcher;

    public Task<TResponse> DispatchAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var method = typeof(IRequestDispatcher).GetMethod(nameof(IRequestDispatcher.DispatchAsync))!
                                               .MakeGenericMethod(request.GetType(), typeof(TResponse));

        return method is null
            ? throw new InvalidOperationException($"No DispatchAsync method found for {request.GetType().Name}")
            : (Task<TResponse>)method.Invoke(_dispatcher, [request, cancellationToken])!;
    }

}
