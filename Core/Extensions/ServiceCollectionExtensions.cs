using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    // extension olabilmesi için static olmalı
    // core katmanı da dahil olmak üzere bütün injectionları buraya ekleyebiliriz
    public static class ServiceCollectionExtensions
    {
        // this parametre anlamında değil neyi genişletmek istediğimizi belirtiriz
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);
        }
    }
}
