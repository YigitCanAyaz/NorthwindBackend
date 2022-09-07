using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    // Hash oluşturmaya ve doğrulamaya yarar
    public class HashingHelper
    {
        // Hash'i oluşturmak + Salt'ı oluşturmak
        // byte arrayi geri döndürecek hem hash hem salt dönecek
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // Key => salt değer, ComputeHash => Hash değeri
                passwordSalt = hmac.Key; // her kullanıcı için farklı key oluşturur
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // string'in byte karşılığını alıoruz Encoding ile birlikte
            }
        }

        // out olmamalı çünkü değeri biz vereceğiz
        // Sonradan sisteme girmek isteyen kişinin verdiği şifrenin bizim veri kaynağımızdaki hash ile ilgili salt'a göre eşleşip eşleşmediğine verdiğimiz yer
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // daha önceden oluşturduğum passwordSalt'ı veriyorum
            // salt'ın key ve hashini oluşturuyoruz
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // hashini oluşturuyoruz

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }

        }
    }
}
