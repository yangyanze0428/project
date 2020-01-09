namespace Web.CusManager.Models
{
    /// <summary>
    /// 当前登录人信息
    /// </summary>
    public class LoginUser
    {
        public string Id { get; set; }
        public string License { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }
}