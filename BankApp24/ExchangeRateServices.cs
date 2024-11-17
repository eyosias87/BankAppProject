namespace BankApp24
{
    public class ExchangeRateService
    {
        private Dictionary<string, decimal> exchangeRates;

        public ExchangeRateService()
        {
            exchangeRates = new Dictionary<string, decimal>
                {
                    { "USD", 1.0m },
                    { "EUR", 1.1m },
                    { "GBP", 1.3m }
                };
        }

        public decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
        {
            if (exchangeRates.ContainsKey(fromCurrency) && exchangeRates.ContainsKey(toCurrency))
            {
                decimal rate = exchangeRates[toCurrency] / exchangeRates[fromCurrency];
                return amount * rate;
            }
            throw new ArgumentException("Currency not supported.");
        }

        public void UpdateExchangeRate(string currency, decimal newRate)
        {
            if (exchangeRates.ContainsKey(currency))
                exchangeRates[currency] = newRate;
        }
    }
}
