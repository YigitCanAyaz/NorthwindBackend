using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    // Her şeyi byte array formatında vermemiz, oluşturmamız gerekiyor
    // Bunları ASP.NET Json Web Token servislerinin anlayacağı şekile getirmemiz gerekiyor
    // appsettings.json'daki SecurityKey'i byte array haline getiriyor
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            // anahtarı oluşturuyor (simetrik olarak) (iki tane var biri asimetrik diğeri simetrik)
            // byte'ını alıp simetrik security key oluşturuyor
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
