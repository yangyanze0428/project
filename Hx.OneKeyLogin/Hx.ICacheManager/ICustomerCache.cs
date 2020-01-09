using Domain.Common.Dtos.MemberShip;

namespace Hx.ICacheManager
{
    public interface ICustomerCache : IDictionaryCache<CustomerDto>
    {
        void Init();
    }
}
