using Domain.Common.Enums;

namespace Domain.Common.Dtos.Perms
{
    public class DataInOrgDto
    {
        public int Id { get; set; }
        public string DataId { get; set; }
        public int OrgId { get; set; }

        public string RoleId { get; set; }
        public DataType DataType { get; set; }
    }
}
