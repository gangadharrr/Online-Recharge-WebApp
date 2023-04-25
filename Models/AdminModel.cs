namespace Online_Recharge_WebApp.Models
{
    public class AdminModel
    {

    }
    public class UserRoleAttr
    {
        public UserRoleAttr(string UserId,string RoleId)
        {
            this.UserId = UserId;
            this.RoleId = RoleId;
        }
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
