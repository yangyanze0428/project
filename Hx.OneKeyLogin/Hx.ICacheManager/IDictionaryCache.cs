using System;
using System.Collections.Generic;

namespace Hx.ICacheManager
{
    public interface IDictionaryCache<TKey, TValue> : IItemdCache<TKey, TValue>
    {
        Dictionary<TKey, TValue> GetDictionary();
    }

    public interface IDictionaryCache<TValue> : IDictionaryCache<string, TValue> { }
}
