using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    // Kullanıcı sisteme istekte bulunurken, eğer yetki gerektiren bir şeyse token'ı vardır
    // Bu token'ı paketin içerisine koyar => buna AccessToken denir
    public class AccessToken
    {
        // Kullanıcı giriş yaptığı zaman bu değerli set edicez
        public string Token { get; set; } // JWT değerinin ta kendisi
        public DateTime Expiration { get; set; } // bitiş zamanı
    }
}
