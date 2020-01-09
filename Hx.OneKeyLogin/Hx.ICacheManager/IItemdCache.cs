using System.Collections.Generic;

namespace Hx.ICacheManager
{
    public interface ISingleCache<T> : ICacheBase
    {
        T Get();
        void Set(T value);
    }

    public interface IItemdCache<in TKey, TValue> : ICacheBase
    {
        /// <summary>
        /// 从缓存中获取指定ID项的值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TValue Get(TKey id);


        /// <summary>
        /// 获取全部缓存项
        /// </summary>
        /// <returns></returns>
        IEnumerable<TValue> GetAll();

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        void Set(TKey id, TValue item);

        /// <summary>
        /// 移除指定ID的缓存项
        /// </summary>
        /// <param name="id"></param>
        void Remove(TKey id);

        /// <summary>
        /// 是否有指定ID的缓存项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistKey(TKey id);
    }
}