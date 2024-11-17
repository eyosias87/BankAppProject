namespace BankApp24
{
    public class SavingsAccount : BankAccount
    {
        public decimal InterestRate { get; private set; }

        public SavingsAccount(string accountNumber, decimal initialBalance, decimal interestRate)
            : base(accountNumber, initialBalance)
        {
            InterestRate = interestRate;
        }

        public decimal CalculateInterest(int months)
        {
            return Balance * (InterestRate / 100) * months / 12;
        }
    }
}
