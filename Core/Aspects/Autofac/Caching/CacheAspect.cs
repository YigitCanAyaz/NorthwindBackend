using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        // Namespace.implementedinterface.methodismi.parametre = key (cachManager'da key var mı diye bak varsa invocation'ın geri dönüşü cachemanagerdan alsın
        // yoksa metod kendisi devam etsin ve _cacheManager ile yeni cache eklesin
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //namespace.implementedinterface
            var arguments = invocation.Arguments.ToList(); // parametre
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed(); // cache yoksa bu kısım çalışır ve alt kodda gözüktüğü gibi yeni cache ekler
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
