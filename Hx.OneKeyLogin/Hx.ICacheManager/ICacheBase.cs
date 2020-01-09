using Hx.Domain.Services;
using Hx.Logging;

namespace Hx.ICacheManager
{
    /// <summary>
    /// 缓存基础类，适用于缓存带ID的同类对象，区别于<see cref="IMixedCacheBase"/>
    /// </summary>
    public interface ICacheBase
    {
        /// <summary>
        /// 
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// 解决UOW拦截器带来的不必要的uow生成、销毁过程带来的性能缺陷
        /// </summary>
        IUnitOfWorkService UnitOfWorkService { get; set; }


        /// <summary>
        /// 清除所有缓存项
        /// </summary>
        void Clear();

        void Rebuild();

    }
}