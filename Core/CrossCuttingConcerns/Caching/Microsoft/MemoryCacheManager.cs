using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        // Adapter Pattern
        IMemoryCache _memoryCache;
        // WebAPI, Business, DataAccess
        // Aspect bambaşka bir zincirin içinde, bağımlılık zincirinin için de değil
        // O yüzden constructorda enjekte edemeyiz

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration)); // TimeSpan => zaman aralığı (o kadar süre için cache'de kalır kod)
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); // bir şey döndürmesini istemiyorsak => _ (sadece bellekte böyle bir anahtar var mı yok mu bunu bulmak istiyorum, datayı istemiyorum
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        // Çalışma anında remove ediyor (reflection ile)
        public void RemoveByPattern(string pattern)
        {
            // Git belleğe bak, (EntriesCollection içine atıyor cache verilerini), entries collectionı bul
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic; // definition _memoryCache olanları bul
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            // her bir cache elemanını gez
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            // bu kurallara uyanlara göre getir
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase); // patternı şu şekil de oluştur
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList(); // gönderdiğim değere uygun olanları keysToRemove olanlara at

            // tek tek kaldır yapıdan
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
