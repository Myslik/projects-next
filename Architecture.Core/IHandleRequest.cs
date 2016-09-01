﻿using System.Threading;
using System.Threading.Tasks;

namespace Architecture.Core
{
    public interface IHandleRequest<in TRequest> : IHandle
        where TRequest : IRequest
    {
        Task Handle(
            TRequest request,
            CancellationToken cancellationToken = default(CancellationToken));
    }

    public interface IHandleRequest<in TRequest, TResponse> : IHandle
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
