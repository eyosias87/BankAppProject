namespace BankApp24
{
    public class BankAccount(string ownerUserName, string accountNumber, decimal initialBalance, string currency)
    {
        private decimal initialBalance = initialBalance;
        private string accountOwnerName = ownerUserName;

        public string OwnerUserName { get; set; } = ownerUserName;
        public string Password { get; private set; } = string.Empty; // Initialize with a default value
        public string AccountNumber { get; set; } = accountNumber;
        public decimal Balance { get; private set; } = initialBalance;
        public string Currency { get; set; } = currency; // Currency for the account

        public BankAccount(string accountNumber, decimal initialBalance)
            : this(string.Empty, accountNumber, initialBalance, string.Empty)
        {
        }

        public BankAccount(string accountOwnerName, string password, string accountNumber, decimal initialBalance, string currency)
            : this(accountOwnerName, accountNumber, initialBalance, currency)
        {
            this.Password = password;
        }

        public BankAccount(string accountOwnerName, string accountNumber, decimal initialBalance)
            : this(accountOwnerName, accountNumber, initialBalance, "USD")
        {
        }

        public bool TransferTo(BankAccount targetAccount, decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                targetAccount.Balance += amount;
                return true;
            }
            return false;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
                return false;
            }

            if (Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawal successful. New balance: {Balance:C}");
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds for this withdrawal.");
                return false;
            }
        }
    }
}