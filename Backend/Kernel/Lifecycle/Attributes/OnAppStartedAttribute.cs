using System;

namespace Backend.Kernel.Lifecycle.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class OnAppStartedAttribute: Attribute
    {
    }
}