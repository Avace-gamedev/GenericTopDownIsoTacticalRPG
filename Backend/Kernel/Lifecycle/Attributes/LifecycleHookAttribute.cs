using System;

namespace Backend.Kernel.Lifecycle.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class LifecycleHookAttribute: Attribute
    {
    }
}