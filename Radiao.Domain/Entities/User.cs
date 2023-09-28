namespace Radiao.Domain.Entities
{
    public class User : Entity
    {
        public string Email { get; private set; }

        public string Password { get; private set; }

        public string Name { get; private set; }

        public bool IsActive { get; private set; } = true;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        private User() {}

        public User(string email, string password, string name, bool isActive = true)
        {
            Email = email;
            Password = password;
            Name = name;
            IsActive = isActive;
        }

        public void HashPassword()
        {
            Password = BCrypt.Net.BCrypt.HashPassword(Password, BCrypt.Net.BCrypt.GenerateSalt(15));
        }

        public bool ValidatePassword(string inputPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, Password);
        }

        public void SetActive()
        {
            IsActive = true;
        }

        public void SetInactive()
        {
            IsActive = false;
        }
    }
}
