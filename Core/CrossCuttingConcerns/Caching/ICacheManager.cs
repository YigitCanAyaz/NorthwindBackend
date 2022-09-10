using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duration);
        bool IsAdd(string key); // Cache'de var mı
        void Remove(string key); // Cache'den kaldır
        void RemoveByPattern(string pattern); // regex pattern (için de get olanlar, category olanlar vs.
    }
}
