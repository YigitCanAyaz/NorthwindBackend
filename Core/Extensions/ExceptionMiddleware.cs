using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";

            IEnumerable<ValidationFailure> errors;

            // eğer aldığımız hata validation exception ise bu mesajı değiştir
            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
                errors = ((ValidationException)e).Errors;
                // sisteme yazıyoruz (aşağıda objeye yazıyoruz), buraya yazmamız lazım
                httpContext.Response.StatusCode = 400;

                // message kısmını yazmayabiliriz
                // sistem ile ilgili bilgi vermememiz lazım
                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = 400,
                    Message = message,
                    ValidationErrors = errors
                }.ToString());

            }

            // vereceğimiz response'u ErrorDetails formatında döndür
            // sistemsel bir yapı, veritabanım çalışmadığında mesela bu hatayı döndürür
                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = message
                }.ToString());
            }
        }
}
