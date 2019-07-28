namespace GSW.Domain.Domain.Users
{
    public class UserClaims
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public RolesEnumType Role { get; set; }
    }
}
