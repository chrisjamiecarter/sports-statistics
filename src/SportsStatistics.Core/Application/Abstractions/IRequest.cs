namespace SportsStatistics.Core.Application.Abstractions;

public interface IRequest
{
}

public interface IRequest<TResponse> : IRequest
{
}
