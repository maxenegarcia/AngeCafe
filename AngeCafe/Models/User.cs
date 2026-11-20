
namespace AngeCafe.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        // Store the hashed password, never the original password.
        public string PasswordHash { get; set; } = string.Empty;

        // Customer, Waiter, or Admin
        public string Role { get; set; } = "Customer";
    }
}

