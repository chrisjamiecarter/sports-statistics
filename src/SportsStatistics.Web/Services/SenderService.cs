using SportsStatistics.Core.Application.Abstractions;

namespace SportsStatistics.Web.Services;

internal interface ISenderService
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}

internal sealed class SenderService(IRequestDispatcher dispatcher) : ISenderService
{
    private readonly IRequestDispatcher _dispatcher = dispatcher;

    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        return _dispatcher.DispatchAsync(request, cancellationToken);
    }
}
