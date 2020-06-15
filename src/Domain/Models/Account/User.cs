namespace Domain.Models.Account
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AccessKey { get; set; }
        public string Role { get; set; }
    }
}