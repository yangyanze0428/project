using Hx.Domain.Entities;

namespace Domain.Common.Entities.MemberShip
{
    public class OrgEntity : AggregateRoot<long>
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int OrderBy { get; set; }
        public int IsEnable { get; set; }
        public string Remark { get; set; }
    }
}
