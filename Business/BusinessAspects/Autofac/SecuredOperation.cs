using Business.Constants;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;

namespace Business.BusinessAspects.Autofac
{
    // JWT için
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        // Her bir kişi için, HttpContext oluşturur (herkese ayrı thread)
        private IHttpContextAccessor _httpContextAccessor;

        // rolleri ver (virgülle ver birden fazlaysa)
        // WindowsForm için burayı değiştireceğimiz zaman Autofac'de yaptığımız injection değerlerini alacak
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            // Autofac ile oluşturduğumuz servis mimarisine ulaş
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        // metodun öncesinde çalışacak
        protected override void OnBefore(IInvocation invocation)
        {
            // rollerini gez, eğer claimin içinde ilgili rol varsa return et (metodu çalıştır)
            // yoksa exception fırlat
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
