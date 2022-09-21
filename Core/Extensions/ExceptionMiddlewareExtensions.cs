using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            // asp.net core yazmış zaten
            // biz sadece o yaşam döngüsünde hangi middleware'ımızı eklemek istiyorsak böyle yazıyoruz
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
