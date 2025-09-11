namespace SportsStatistics.Core.Application.Abstractions;

public interface IRequestDispatcher
{
    //Task DispatchAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) 
    //    where TRequest : IRequest;
    
    Task<TResponse> DispatchAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) 
        where TRequest : IRequest<TResponse>;

    Task<TResponse> DispatchAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}