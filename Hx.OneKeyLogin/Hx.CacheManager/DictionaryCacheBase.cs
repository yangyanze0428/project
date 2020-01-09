using System.Collections.Generic;
using System.Linq;
using Hx.Components;
using Hx.ICacheManager;
using Hx.Domain.Services;
using Hx.Extensions;
using Hx.Logging;

namespace Hx.CacheManager
{
    /// <summary>
    /// 字典缓存,适用于系统启动时整体缓存的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PropertyWired]
    public abstract class DictionaryCacheBase<T> : IDictionaryCache<string, T>
    {
        private bool _hasInited;

        public ILogger Logger { get; set; }

        /// <summary>
        /// 解决UOW拦截器带来的不必要的uow生成、销毁过程带来的性能缺陷
        /// </summary>
        public IUnitOfWorkService UnitOfWorkService { get; set; }


        private readonly Dictionary<string, T> _dictionary = new Dictionary<string, T>();

        /// <inheritdoc />
        public T Get(string id)
        {
            if (!_hasInited) DoInit();
            if (_dictionary.TryGetValue(id, out var value))
            {
                return value;
            }
            return default(T);
        }

        /// <inheritdoc />
        public IEnumerable<T> GetAll()
        {
            if (!_hasInited) DoInit();
            return _dictionary.Select(item => item.Value);
        }

        /// <inheritdoc />
        public virtual void Set(string id, T item)
        {
            if (!_hasInited) DoInit();

            _dictionary[id] = item;
        }

        /// <inheritdoc />
        public virtual void Remove(string id)
        {
            if (!_hasInited) return;

            _dictionary.Remove(id);
        }

        /// <inheritdoc />
        public bool ExistKey(string id)
        {
            if (!_hasInited) DoInit();

            return _dictionary.ContainsKey(id);
        }

        /// <inheritdoc />
        public void Clear()
        {
            if (!_hasInited) return;
            _dictionary.Clear();
            _hasInited = false;
        }


        private void DoInit()
        {
            if (_hasInited) return;
            _dictionary.Locking(d =>
            {
                if (_hasInited) return;
                UnitOfWorkService.ExecuteNonQuery(() => Create(_dictionary));
                _hasInited = true;
            });
        }

        /// <summary>
        /// 缓存初始化
        /// </summary>
        protected abstract void Create(Dictionary<string, T> dict);

        public Dictionary<string, T> GetDictionary()
        {
            if (!_hasInited) DoInit();
            return _dictionary;
        }

        public void Rebuild()
        {
            Clear();
            DoInit();
        }
    }
}