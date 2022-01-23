using System;

namespace Backend.Kernel.Lifecycle.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OnAppStartingAttribute: Attribute
    {
    }
}