using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hx.Domain.Entities;

namespace Domain.Common.Dtos.Perms
{
    public class RoleInPermDto
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string PermId { get; set; }
        public string Operation { get; set; }
    }
}
