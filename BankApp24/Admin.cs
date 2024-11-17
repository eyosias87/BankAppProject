namespace BankApp24
{
    public class Admin : User
    {
        private List<User> users = new List<User> { new User("Eyosias", "Password123!") };
        private readonly Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>();

        public Admin(string username, string password) : base(username, password)
        {

        }

        public void CreateNewUser(string username, string password)
        {
            // Logic to add a new user to the system
            var newUser = new User(username, password);
            newUser.SetAdmin(true);
            users.Add(newUser);
        }

        public void UpdateExchangeRate(string currency, decimal newRate)
        {
            // Logic to update exchange rate
            exchangeRates[currency] = newRate;
        }
    }
}




