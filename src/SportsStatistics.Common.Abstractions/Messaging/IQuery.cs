using MediatR;
using SportsStatistics.Common.Primitives.Results;

namespace SportsStatistics.Common.Abstractions.Messaging;

/// <summary>
/// Defines a query request that returns a <see cref="Result"> response of type <see cref="TResponse".
/// </summary>
/// <typeparam name="TResponse">The command response type.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
