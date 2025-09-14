using MediatR;
using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Web.Abstractions.Messaging;

internal sealed class Messenger(ISender sender) : IMessenger
{
    private readonly ISender _sender = sender;

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        return await _sender.Send(request, cancellationToken);
    }
}
