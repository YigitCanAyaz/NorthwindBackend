using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        // Uygulamaya ayağa kalktığında burası çalışacak
        protected override void Load(ContainerBuilder builder)
        {
            // Single Instance =>  Tek bir instance oluşturur (herkese aynı referans)
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            
            // çalışan uygulama içerisinde
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            // implement edilmiş interfaceleri bul
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() // onlar için aspectinterceptorselector'ı çağır
                }).SingleInstance();
        }
    }
}
