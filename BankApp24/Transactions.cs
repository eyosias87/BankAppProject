namespace BankApp24
{
    public class Transaction
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Transaction(string fromAccount, string toAccount, decimal amount, DateTime now, string description)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Amount = amount;
            Date = DateTime.Now;
            Description = description;
        }
    }
}
