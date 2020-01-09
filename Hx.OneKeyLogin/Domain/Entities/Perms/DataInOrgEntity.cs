using Hx.Domain.Entities;

namespace Domain.Common.Entities.Perms
{
    public class DataInOrgEntity : AggregateRoot<long>
    {
        public string DataId { get; set; }
        public int OrgId { get; set; }
        public string RoleId { get; set; }
        public int DataType { get; set; }
    }
}
