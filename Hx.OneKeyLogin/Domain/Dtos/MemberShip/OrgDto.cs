using System.Collections.Generic;

namespace Domain.Common.Dtos.MemberShip
{
    public class OrgDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int OrderBy { get; set; }
        public int IsEnable { get; set; }
        public string Remark { get; set; }
        public List<OrgDto> Children { get; set; } = new List<OrgDto>();
    }

    public class OrgDtoShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int OrderBy { get; set; }
        public int IsEnable { get; set; }
        public string Remark { get; set; }
        public List<OrgDtoShow> Children { get; set; } = new List<OrgDtoShow>();
    }
}
