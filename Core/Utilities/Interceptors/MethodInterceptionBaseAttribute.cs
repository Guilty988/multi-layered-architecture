using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
                   // Hem class hemde methodlarda birden fazla ve inherited edilebilen durumlarda kullanılsın işaretlemesi
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }

}

