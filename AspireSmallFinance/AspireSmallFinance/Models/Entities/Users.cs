namespace AspireSmallFinance.Models.Entities
{
    public class Users
    {
        public int UserSysId { get; set; }
        public string? UserName { get; set; }
        public string? UserFullName { get; set; }
        public bool IsAdminFlag { get; set; }
    }
}
