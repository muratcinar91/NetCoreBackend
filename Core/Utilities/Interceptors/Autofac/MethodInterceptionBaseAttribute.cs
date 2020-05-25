using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors.Autofac
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple = true,Inherited = true)]
    public  abstract class MethodInterceptionBaseAttribute : Attribute,IInterceptor
    {
        public int Priority { get; set; }

        //virtual override edilebilmesi için eklenmiştir.
        public virtual void Intercept(IInvocation invocation)
        {
            
        }
    }
}
