using System;

namespace Local.Api.Common.Entities
{
    public class Users
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
