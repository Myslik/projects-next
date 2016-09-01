using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.Core
{
    public class Bus : IBus
    {
        private readonly HandlerProvider _handlerProvider;

        public Bus(IServiceProvider serviceProvider)
        {
            _handlerProvider = new HandlerProvider(serviceProvider);
            _handlerProvider.HandlerCreated += OnHandlerCreated;
        }

        [DebuggerStepThrough, DebuggerHidden]
        public virtual async Task Send(IEvent @event, CancellationToken cancellationToken)
        {
            Guard.AgainstNull(nameof(@event), @event);
            Validate(@event);
            var messageType = @event.GetType();
            var handlers = _handlerProvider.GetEventHandlers(messageType);
            foreach (var handler in handlers)
            {
                await handler.Handle(@event, cancellationToken);
            }
        }

        [DebuggerStepThrough, DebuggerHidden]
        public virtual async Task Send(IRequest request, CancellationToken cancellationToken)
        {
            Guard.AgainstNull(nameof(request), request);
            Validate(request);
            var requestType = request.GetType();
            var handler = _handlerProvider.GetRequestHandler(requestType);
            await handler.Handle(request, cancellationToken);
        }

        [DebuggerStepThrough, DebuggerHidden]
        public virtual async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            Guard.AgainstNull(nameof(request), request);
            Validate(request);
            var requestType = request.GetType();
            var handler = _handlerProvider.GetRequestHandler<TResponse>(requestType);
            return await handler.Handle(request, cancellationToken);
        }

        protected virtual void Validate(IMessage message)
        {
            var validationContext = new ValidationContext(message);
            Validator.ValidateObject(message, validationContext);
        }

        private void OnHandlerCreated(object sender, HandlerCreatedEventArgs e)
        {
            var handler = e.Handler;
            if (typeof(MessageHandler).IsAssignableFrom(handler.GetType()))
            {
                InjectHandler((MessageHandler)handler);
            }
        }

        private void InjectHandler(MessageHandler handler)
        {
            handler.Bus = this;
        }
    }
}
