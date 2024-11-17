namespace BankApp24
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int LoginAttempts { get; private set; } = 0;
        public bool IsLockedOut => LoginAttempts >= 3;
        public bool IsBlocked { get; private set; } = false;
        public bool IsAdmin { get; private set; }

        // Constructor
        public User(string username, string password, bool isAdmin = false)
        {
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
            IsBlocked = false;
        }

        // Method to set admin status
        public void SetAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        // Password verification logic
        public bool VerifyPassword(string inputPassword)
        {
            if (IsBlocked)
            {
                Console.WriteLine("Account is blocked. Please contact support.");
                return false;
            }

            if (IsLockedOut)
            {
                Console.WriteLine("Account is locked due to multiple failed login attempts.");
                return false;
            }

            if (Password == inputPassword)
            {
                LoginAttempts = 0; // Reset attempts on successful login
                return true;
            }
            else
            {
                LoginAttempts++;
                if (IsLockedOut)
                {
                    Console.WriteLine("Account is now locked due to three failed login attempts.");
                }
                return false;
            }
        }

        // Method to block the user
        public void BlockUser()
        {
            IsBlocked = true;
            Console.WriteLine($"User '{Username}' has been blocked.");
        }

        // Method to unblock the user
        public void UnblockUser()
        {
            IsBlocked = false;
            LoginAttempts = 0; // Reset login attempts upon unblocking
            Console.WriteLine($"User '{Username}' has been unblocked.");
        }
    }
}
