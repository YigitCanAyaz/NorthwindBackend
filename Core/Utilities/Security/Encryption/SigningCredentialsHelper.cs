using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        // ASP.NET'in, WEB'in kendisinin de ihtiyacı var
        // Bu sınıf hangi anahtarı kullanıp hangi algoritma kullanılacak bunu yazıyor
        // Farklı algoritmalar => Farklı versiyonlar (yenisini kullanmak eski bir database yoksa daha iyi)
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            // anahtar olarak bu securtiyKey'i kullan, şifreleme olarak bu algoritmayı kullan
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
