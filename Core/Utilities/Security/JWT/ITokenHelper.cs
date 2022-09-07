using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        // Kullanıcı adı ve şifre girildikten sonra bu operasyon çalışılacak
        // Veritabanına gidecek, veritabanında bu kullanıcıların claim'larini bulacak
        // Bir JWT oluşturacak ve Client'a gönderecek
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
