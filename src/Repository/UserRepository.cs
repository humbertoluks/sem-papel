using Domain.Models.Account;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public static class UserRepository
    {
        public static User Get(string username, string accessKey)
        {
            var users = new List<User>();

            users.Add(new User { Id = 1, UserName = "Andromeda", AccessKey = "5XWElFOM", Role = "employee"});
            users.Add(new User { Id = 2, UserName = "Pegasus", AccessKey = "1234", Role = "employee"});
            
            return users.Where(x => x.UserName.ToLower() == username.ToLower() && x.AccessKey == accessKey).FirstOrDefault();
        }
    }
} 