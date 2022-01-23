using System;

namespace Backend.Kernel.Lifecycle.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class LifecycleHookAttribute: Attribute
    {
    }
}