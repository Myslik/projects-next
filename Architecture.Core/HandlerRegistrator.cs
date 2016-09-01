using System;
using System.Linq;

namespace Architecture.Core
{
    public delegate void RegisterAction
        (Type serviceType, Type implementationType);

    public class HandlerRegistrator
    {
        private const string HANDLER_NOT_SUPPORTED =
            "Provided handler type is not supported.";

        private RegisterAction _registerAction;

        public HandlerRegistrator(RegisterAction registerAction)
        {
            _registerAction = registerAction;
        }

        public void RegisterHandler(Type handlerType)
        {
            var handlers = GetImplementedType(handlerType)
                .Select(t => new TypeInfo(t))
                .Where(i => i.IsHandler)
                .ToArray();

            var count = handlers.Count();
            if (count == 0)
            {
                throw new InvalidOperationException(HANDLER_NOT_SUPPORTED);
            }

            foreach (var handler in handlers)
            {
                _registerAction.Invoke(handler.ServiceType, handlerType);
            }
        }

        public void RegisterHandler<THandler>()
            where THandler : IHandle
        {
            RegisterHandler(typeof(THandler));
        }

        private static Type[] GetImplementedType(Type type)
        {
            return type.GetInterfaces();
        }

        private class TypeInfo
        {
            public TypeInfo(Type t)
            {
                IsGenericType = t.IsGenericType;
                if (IsGenericType)
                {
                    GenericTypeDefinition = t.GetGenericTypeDefinition();
                    GenericTypeArguments = t.GenericTypeArguments;
                    IsEventHandler =
                        GenericTypeDefinition == typeof(IHandleEvent<>);
                    bool isRequestHandler1 =
                        GenericTypeDefinition == typeof(IHandleRequest<>);
                    bool isRequestHandler2 =
                        GenericTypeDefinition == typeof(IHandleRequest<,>);
                    IsRequestHandler =
                        isRequestHandler1 ||
                        isRequestHandler2;
                    if (IsEventHandler)
                    {
                        ServiceType = typeof(IHandleEvent<>)
                            .MakeGenericType(GenericTypeArguments);
                    }
                    else if (isRequestHandler1)
                    {
                        ServiceType = typeof(IHandleRequest<>)
                            .MakeGenericType(GenericTypeArguments);
                    }
                    else if (isRequestHandler2)
                    {
                        ServiceType = typeof(IHandleRequest<,>)
                            .MakeGenericType(GenericTypeArguments);
                    }
                }
            }

            public bool IsEventHandler { get; private set; }
            public bool IsRequestHandler { get; private set; }
            public bool IsHandler
            {
                get { return IsEventHandler || IsRequestHandler; }
            }
            public Type ServiceType { get; private set; }

            private bool IsGenericType { get; set; }
            private Type GenericTypeDefinition { get; set; }
            private Type[] GenericTypeArguments { get; set; }
        }
    }
}
