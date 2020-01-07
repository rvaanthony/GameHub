using GameHub.Classes;
using System;

namespace GameHub.Models
{
    public class UserModel : BaseModel
    {
        public int Id { get; set; }
        public string GUID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Enums.UserStatus Status { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool Active { get; set; }
        public int ClientId { get; set; }
        public UserContextModel UserContext { get; set; }
    }

}
