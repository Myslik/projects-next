using System;

namespace Architecture.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class EnableHandlerAttribute : Attribute
    {
    }
}