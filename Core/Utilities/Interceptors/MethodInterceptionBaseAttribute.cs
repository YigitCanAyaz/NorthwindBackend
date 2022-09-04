using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    // Hem classlar hem methodlara uygulanabilir
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        // Öncelik sıralaması yapacağımız property
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
