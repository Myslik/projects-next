using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.Core
{
    internal delegate void HandlerCreatedEventHandler
        (object sender, HandlerCreatedEventArgs e);

    internal class HandlerCreatedEventArgs : EventArgs
    {
        internal HandlerCreatedEventArgs(object handler)
        {
            Handler = handler;
        }

        internal object Handler { get; }
    }

    internal sealed class HandlerProvider
    {
        private const string HANDLER_NOT_FOUND =
            "Handler was not found for request of type ";

        private readonly IServiceProvider _serviceProvider;

        internal event HandlerCreatedEventHandler HandlerCreated;

        private void OnHandlerCreated(object handler)
        {
            HandlerCreated?.Invoke(this, new HandlerCreatedEventArgs(handler));
        }

        internal HandlerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #region Events
        [DebuggerStepThrough, DebuggerHidden]
        internal IEnumerable<EventHandler> GetEventHandlers(Type eventType)
        {
            var serviceType = typeof(IHandleEvent<>).MakeGenericType(eventType);
            var handlers = _serviceProvider.GetServices(serviceType);
            if (handlers != null)
            {
                foreach (var handler in handlers)
                {
                    OnHandlerCreated(handler);
                    yield return WrapEventHandler(eventType, handler);
                }
            }
        }

        private static EventHandler WrapEventHandler(Type eventType, object handler)
        {
            var wrapperType = typeof(EventHandler<>).MakeGenericType(eventType);
            return (EventHandler)Activator.CreateInstance(wrapperType, handler);
        }

        internal abstract class EventHandler
        {
            public abstract Task Handle(IEvent @event, CancellationToken cancellationToken);
        }

        internal sealed class EventHandler<TEvent> : EventHandler
            where TEvent : IEvent
        {
            private readonly IHandleEvent<TEvent> _inner;

            public EventHandler(IHandleEvent<TEvent> inner)
            {
                _inner = inner;
            }

            [DebuggerStepThrough, DebuggerHidden]
            public override async Task Handle(IEvent @event, CancellationToken cancellationToken)
            {
                await _inner.Handle((TEvent)@event, cancellationToken);
            }
        }
        #endregion

        #region Requests
        [DebuggerStepThrough, DebuggerHidden]
        internal RequestHandler GetRequestHandler(Type requestType)
        {
            var serviceType = typeof(IHandleRequest<>).MakeGenericType(requestType);
            object handler;
            try
            {
                handler = _serviceProvider.GetService(serviceType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(HANDLER_NOT_FOUND + requestType, ex);
            }
            if (handler == null)
            {
                throw new InvalidOperationException(HANDLER_NOT_FOUND + requestType);
            }
            OnHandlerCreated(handler);
            return WrapRequestHandler(requestType, handler);
        }

        [DebuggerStepThrough, DebuggerHidden]
        internal RequestHandlerWithResponse<TResponse> GetRequestHandler<TResponse>(Type requestType)
        {
            var serviceType = typeof(IHandleRequest<,>).MakeGenericType(requestType, typeof(TResponse));
            object handler;
            try
            {
                handler = _serviceProvider.GetService(serviceType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(HANDLER_NOT_FOUND + requestType, ex);
            }
            if (handler == null)
            {
                throw new InvalidOperationException(HANDLER_NOT_FOUND + requestType);
            }
            OnHandlerCreated(handler);
            return WrapRequestHandlerWithResponse<TResponse>(requestType, handler);
        }

        private static RequestHandler WrapRequestHandler(Type requestType, object handler)
        {
            var wrapperType = typeof(RequestHandler<>).MakeGenericType(requestType);
            return (RequestHandler)Activator.CreateInstance(wrapperType, handler);
        }

        internal abstract class RequestHandler
        {
            public abstract Task Handle(IRequest request, CancellationToken cancellationToken);
        }

        internal sealed class RequestHandler<TRequest> : RequestHandler
            where TRequest : IRequest
        {
            private readonly IHandleRequest<TRequest> _inner;

            public RequestHandler(IHandleRequest<TRequest> inner)
            {
                _inner = inner;
            }

            [DebuggerStepThrough, DebuggerHidden]
            public override async Task Handle(IRequest request, CancellationToken cancellationToken)
            {
                await _inner.Handle((TRequest)request, cancellationToken);
            }
        }

        private static RequestHandlerWithResponse<TResponse> WrapRequestHandlerWithResponse<TResponse>(Type requestType, object handler)
        {
            var wrapperType = typeof(RequestHandlerWithResponse<,>).MakeGenericType(requestType, typeof(TResponse));
            return (RequestHandlerWithResponse<TResponse>)Activator.CreateInstance(wrapperType, handler);
        }

        internal abstract class RequestHandlerWithResponse<TResponse>
        {
            public abstract Task<TResponse> Handle(IRequest<TResponse> request, CancellationToken cancellationToken);
        }

        internal sealed class RequestHandlerWithResponse<TRequest, TResponse> : RequestHandlerWithResponse<TResponse>
            where TRequest : IRequest<TResponse>
        {
            private readonly IHandleRequest<TRequest, TResponse> _inner;

            public RequestHandlerWithResponse(IHandleRequest<TRequest, TResponse> inner)
            {
                _inner = inner;
            }

            [DebuggerStepThrough, DebuggerHidden]
            public override async Task<TResponse> Handle(IRequest<TResponse> request, CancellationToken cancellationToken)
            {
                return await _inner.Handle((TRequest)request, cancellationToken);
            }
        }
        #endregion
    }
}
