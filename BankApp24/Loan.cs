namespace BankApp24
{
    public class Loan
    {
        public string LoanType { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int RepaymentMonths { get; set; }
        public string BorrowerUsername { get; set; }

        public Loan(string loanType, decimal amount, decimal interestRate, int repaymentMonths, string borrowerUsername)
        {
            LoanType = loanType;
            Amount = amount;
            InterestRate = interestRate;
            RepaymentMonths = repaymentMonths;
            BorrowerUsername = borrowerUsername;
        }

        public decimal CalculateMonthlyPayment()
        {
            var monthlyRate = InterestRate / 12 / 100;
            return Amount * monthlyRate / (1 - (decimal)Math.Pow((double)(1 + monthlyRate), -RepaymentMonths));
        }
    }
}
