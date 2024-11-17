using System.Security.Cryptography;
using System.Text;

namespace BankApp24
{
    public class BankSystem
    {
        private List<User> users;
        private List<BankAccount> accounts;
        private List<Transaction> transactionLog;
        private Queue<Transaction> pendingTransactions;
        private Dictionary<string, decimal> exchangeRates;
        private List<Loan> loans;


        public BankSystem()
        {
            users = new List<User>(); // Initialize the users list
            {
                //Add the admin user
                users.Add(new User("Eyosias", "Password123!", true));//Admin user

                // Add default users and accounts
                users.Add(new User("Hemen", "Hemen123!"));//Non-admin user
                users.Add(new User("Alice", "Alice123!"));//Non-admin user
                users.Add(new User("Bob", "Bob123!"));//Non-admin user
                users.Add(new User("Charlie", "Charlie123!"));//Non-admin user
                users.Add(new User("Mariam", "Mariam123!"));//Non-admin user
                users.Add(new User("Kebede", "Kebede123!"));//Non-admin user
                users.Add(new User("Tsegaye", "Tsegaye123!"));//Non-admin user
            };
            accounts = new List<BankAccount>(); // Initialize the accounts list
            {
                accounts.Add(new BankAccount("Hemen", "678901", 5000, "EUR"));
                accounts.Add(new BankAccount("Alice", "234567", 750, "GBP"));
                accounts.Add(new BankAccount("Bob", "345678", 3000, "USD"));
                accounts.Add(new BankAccount("Charlie", "456789", 1200, "EUR"));
                accounts.Add(new BankAccount("Mariam", "234567", 3000, "GBP"));
                accounts.Add(new BankAccount("Kebede", "345678", 1500, "USD"));
                accounts.Add(new BankAccount("Tsegaye", "456789", 2000, "EUR"));
            };

            loans = new List<Loan>(); // Initialize the loans list
            {
                loans.Add(new Loan("Housing", 250000, 3.5m, 240, "Eyosias"));
                loans.Add(new Loan("Vehicle", 20000, 5.0m, 60, "Hemen"));
                loans.Add(new Loan("Personal", 5000, 6.0m, 36, "Alice"));
            };
            transactionLog = new List<Transaction>(); // Initialize the transactionLog list

            DisplayBankLogo();

            // Initialize exchange rates
            exchangeRates = new Dictionary<string, decimal>
            {
                { "USD", 1.0m }, // Assuming USD as the base currency
                { "EUR", 0.85m },
                { "GBP", 0.75m }
            };

            pendingTransactions = new Queue<Transaction>(); // Initialize the pendingTransactions queue

            ShowMainMenu();
            ShowAdminMenu();
            ShowCustomerMenu();
            ReadPassword();
            LoadUserData();
            SaveUserData();
            TransferFunds();

            // Set up the timer for scheduled transactions
            transactionTimer = new System.Threading.Timer(ProcessScheduledTransactions, null, 0, 180000); // 3 minutes
        }

        private List<string> loanTypes = new List<string> { "Housing", "Vehicle", "Education", "Personal", "Other" };
        private Dictionary<string, (decimal interestRate, int repaymentMonths)> loanTerms = new Dictionary<string, (decimal, int)>
        {
            { "Housing", (3.5m, 240) },
            { "Vehicle", (5.0m, 60) },
            { "Education", (4.5m, 120) },
            { "Personal", (6.0m, 36) },
            { "Other", (7.5m, 24) }
        };
        private void DisplayBankLogo()
        {
            Console.Clear();
            Console.WriteLine(@"
       ____  _                 _             _     
      |  _ \| |               | |           (_)    
      | |_) | | __ _ _ __   __| | ___  _ __  _ ___ 
      |  _ <| |/ _` | '_ \ / _` |/ _ \| '_ \| / __|
      | |_) | | (_| | | | | (_| | (_) | | | | \__ \
      |____/|_|\__,_|_| |_|\__,_|\___/|_| |_|_|___/
            
            ~ Welcome to Chasers Bank SE ~
    ");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Customer Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AdminLogin();
                        break;
                    case "2":
                        CustomerLogin();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to return to the Main Menu...");
                Console.ReadKey();
            }
        }

        private void AdminLogin()
        {
            Console.Clear();
            Console.Write("Admin username: ");
            string? username = Console.ReadLine();
            string password = ReadPassword();

            // Mock admin credentials for demonstration
            string adminUsername = "Eyosias";
            string adminPasswordHash = HashPassword("Password123!");


            if (username == adminUsername && ValidatePassword(password, adminPasswordHash))
            {
                Console.WriteLine("\nAdmin login successful!");
                Console.WriteLine("Welcome!");
                currentUser = new User(adminUsername, "Password123!", true); // Set role as Admin
                ShowAdminMenu();
                // Proceed to admin menu
            }
            else
            {
                Console.WriteLine("\nInvalid admin credentials.");
            }

        }


        private string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;

            Console.Write("Enter password: ");
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            }

            Console.WriteLine();
            return password.ToString();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool ValidatePassword(string enteredPassword, string storedHashedPassword)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword);
            return hashedEnteredPassword == storedHashedPassword;
        }

        private User? currentUser;

        public void ValidateAdminPrivileges()
        {
            if (currentUser == null || !currentUser.IsAdmin)
            {
                throw new UnauthorizedAccessException("Admin privileges are required.");
            }
        }
        // Method to show the admin menu
        public void ShowAdminMenu()
        {
            while (true) // Keep the admin menu active until the admin logs out
            {

                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Create New Account");
                Console.WriteLine("2. Remove or Block Customer");
                Console.WriteLine("3. View All User Accounts");
                Console.WriteLine("4. View All Customer Loan Status");
                Console.WriteLine("5. Update Customer Loan Interest Rates");
                Console.WriteLine("6. Update Exchange Rates");
                Console.WriteLine("7. Logout");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();
                try
                {

                    switch (choice)
                    {
                        case "1":
                            CreateNewAccount();
                            break;
                        case "2":
                            RemoveOrBlockCustomer();
                            break;
                        case "3":
                            ViewAllCustomerAccounts();
                            break;
                        case "4":
                            ViewAllCustomerLoanStatus();
                            break;
                        case "5":
                            UpdateCustomerLoanInterestRates();
                            break;
                        case "6":
                            UpdateExchangeRates();
                            break;
                        case "7":
                            LogoutAdmin();
                            break; // Indicate to exit the menu loop
                        default:
                            Console.WriteLine("Invalid selection. Please try again.");
                            break;
                    }

                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to return to the Admin Menu...");
                Console.ReadKey();
            }
        }
        private void CreateNewAccount()
        {
            ValidateAdminPrivileges();
            Console.Clear();
            Console.WriteLine("Enter a unique account number:");
            string? accountNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(accountNumber) || accounts.Any(account => account.AccountNumber == accountNumber))
            {
                Console.WriteLine("Invalid or duplicate account number.");
                return;
            }

            Console.WriteLine("Enter initial balance:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance) || initialBalance < 0)
            {
                Console.WriteLine("Invalid initial balance.");
                return;
            }

            Console.WriteLine("Enter currency (e.g., USD, EUR, SEK):");
            string? currency = Console.ReadLine();

            if (string.IsNullOrEmpty(currency) || !exchangeRates.ContainsKey(currency.ToUpper()))
            {
                Console.WriteLine("Invalid currency.");
                return;
            }

            var newAccount = new BankAccount(currentUser.Username, accountNumber, initialBalance, currency.ToUpper());
            accounts.Add(newAccount);

            Console.WriteLine($"New account created successfully with Account Number: {accountNumber}");
        }


        private void RemoveOrBlockCustomer()
        {
            ValidateAdminPrivileges();
            Console.Clear();
            Console.WriteLine("\nRemove or Block Customer:");
            Console.Write("Enter Customer's Name: ");
            string? customerName = Console.ReadLine();

            var userToRemove = users.FirstOrDefault(u => u.Username == customerName && !u.IsAdmin);

            if (userToRemove == null)
            {
                Console.WriteLine("Customer not found or is an admin.");
                return;
            }

            users.Remove(userToRemove);
            accounts.RemoveAll(a => a.OwnerUserName == customerName);
            loans.RemoveAll(l => l.BorrowerUsername == customerName); // Updated line

            Console.WriteLine($"Customer {customerName} and associated data removed successfully.");
        }
        private void ViewAllCustomerAccounts()
        {
            ValidateAdminPrivileges();
            Console.Clear();
            Console.WriteLine("\nAll Customer Accounts:");
            Console.WriteLine("{0,-20} {1,-15} {2,-15} {3,-15} {4,-10}", "Username", "Is Admin", "Account Number", "Account Type", "Balance", "Currency");
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts found.");
            }
            else
            {
                foreach (var account in accounts)
                {
                    Console.WriteLine("{0,-20} {1,-15} {2,-15} {3,-15} {4,-10}",
                    account.OwnerUserName,
                    users.FirstOrDefault(u => u.Username == account.OwnerUserName)?.IsAdmin.ToString() ?? "False",
                    account.AccountNumber,
                    account.GetType().Name, // Assuming AccountType is based on the account class type
                    account.Balance.ToString("C"),
                    account.Currency);
                }

            }
        }
        private void ViewAllCustomerLoanStatus()
        {
            ValidateAdminPrivileges();
            Console.Clear();
            Console.WriteLine("\nAll Customer Loan Status:");
            if (loans.Count == 0)
            {
                Console.WriteLine("No loans found.");
            }
            else
            {
                foreach (var loan in loans)
                {
                    Console.WriteLine($"Loan Type: {loan.LoanType}, Amount: {loan.Amount}, Interest Rate: {loan.InterestRate}%, Duration: {loan.RepaymentMonths} months, Borrower: {loan.BorrowerUsername}");
                }

            }
        }
        public void UpdateCustomerLoanInterestRates()
        {
            ValidateAdminPrivileges();
            Console.Clear();
            Console.WriteLine("\nUpdate Loan Interest Rates:");

            Console.Write("Enter Borrower's Name: ");
            string? borrowerName = Console.ReadLine();

            var customerLoans = loans.Where(l => l.BorrowerUsername == borrowerName).ToList();

            if (customerLoans.Count == 0)
            {
                Console.WriteLine("No loans found for the specified borrower.");
                return;
            }

            Console.WriteLine($"Loans for {borrowerName}:");
            Console.WriteLine("{0,-20} {1,-15} {2,-10} {3,-10} {4,-15}",
            "Customer Name", "Loan Type", "Amount", "Current Interest", "Duration (Months)");
            foreach (var loan in customerLoans)
            {
                Console.WriteLine("{0,-20} {1,-15} {2,-10} {3,-10} {4,-15}",
                loan.BorrowerUsername, loan.LoanType, loan.Amount, $"{loan.InterestRate}%", loan.RepaymentMonths);
            }

            Console.Write("Enter New Interest Rate: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal newRate) && newRate > 0)
            {
                foreach (var loan in customerLoans)
                {
                    loan.InterestRate = newRate;
                }
                Console.WriteLine("Interest rates updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid interest rate.");
            }
        }
        private void UpdateExchangeRates()
        {
            ValidateAdminPrivileges();
            Console.Clear();
            Console.WriteLine("Update Exchange Rates:");
            foreach (var currency in exchangeRates.Keys.ToList())
            {
                Console.WriteLine($"Current rate for {currency}: {exchangeRates[currency]} (relative to USD)");
                Console.Write($"Enter new rate for {currency} (or press Enter to keep current rate): ");

                string? input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal newRate) && newRate > 0)
                {
                    exchangeRates[currency] = newRate;
                    Console.WriteLine($"Rate for {currency} updated to {newRate}");
                }
                else if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"Rate for {currency} remains unchanged.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Skipping update for this currency.");
                }
            }

            Console.WriteLine("Exchange rates updated successfully.");
            Console.WriteLine("Press any key to return to the Admin Menu.");
            Console.ReadKey();

        }
        private void LogoutAdmin()
        {
            Console.Clear();
            Console.WriteLine("Logging out...");
            ShowMainMenu();
        }



        public void DisplayAvailableLoans()
        {
            Console.Clear();
            Console.WriteLine("Available Loan Options:");
            foreach (var loanType in loanTypes)
            {
                var loanTerm = loanTerms[loanType];
                Console.WriteLine($"{loanType}: Interest Rate: {loanTerm.interestRate}%, Repayment Months: {loanTerm.repaymentMonths}");
            }
        }


        private Timer transactionTimer;
        private void CustomerLogin()
        {
            Console.Clear();
            Console.WriteLine("Customer Login");

            Console.Write("Enter Username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string? password = ReadPassword(); // Use the same ReadPassword method as in AdminLogin.

            // Check if the user exists and is not an admin.
            var customer = users.FirstOrDefault(u => u.Username == username && u.Password == password && !u.IsAdmin);

            if (customer != null)
            {
                currentUser = customer;
                Console.WriteLine($"Welcome, {currentUser.Username}!");
                ShowCustomerMenu(); // Navigate to the customer menu.
            }
            else
            {
                Console.WriteLine("Invalid login credentials or you are not a customer. Please try again.");
            }
        }

        private void ShowCustomerMenu()
        {
            Console.Clear();
            if (currentUser == null || currentUser.IsAdmin)
            {
                Console.WriteLine("Access denied. Only customers can access this menu.");
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Customer Menu:");
                Console.WriteLine("1. View Account Summary");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Open New Account");
                Console.WriteLine("5. Check Loan Status");
                Console.WriteLine("6. Request Loan");
                Console.WriteLine("7. Display Available Loans");
                Console.WriteLine("8. View Transaction History");
                Console.WriteLine("9. Logout");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ViewAccountSummary();
                        break;
                    case "2":
                        Deposit();
                        break;
                    case "3":
                        Withdraw();
                        break;
                    case "4":
                        OpenNewAccount();
                        break;
                    case "5":
                        CheckLoanStatus();
                        break;
                    case "6":
                        RequestLoan();
                        break;
                    case "7":
                        DisplayAvailableLoans();
                        break;
                    case "8":
                        ViewTransactionHistory();
                        break;
                    case "9":
                        Console.WriteLine("Logging out...");
                        currentUser = null; // Clear current user on logout.
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }


        private void ViewAccountSummary()
        {
            Console.Clear();
            var customerAccounts = accounts.Where(a => a.OwnerUserName == currentUser.Username).ToList();

            if (customerAccounts.Count == 0)
            {
                Console.WriteLine("No accounts found.");
                return;
            }

            Console.WriteLine("\nYour Account Summary:");
            foreach (var account in customerAccounts)
            {
                Console.WriteLine($"Account Number: {account.AccountNumber}, Balance: {account.Balance} {account.Currency}");
            }
        }

        private void Deposit()
        {
            Console.Clear();
            Console.Write("Enter Account Number: ");
            string? accountNumber = Console.ReadLine();

            var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber && a.OwnerUserName == currentUser.Username);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.Write("Enter Amount to Deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                account.Deposit(amount);
                Console.WriteLine($"Successfully deposited {amount}. New Balance: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }



        public void Withdraw()
        {
            Console.Clear();
            Console.WriteLine("Withdraw:");
            if (currentUser == null)
            {
                Console.WriteLine("You must be logged in to perform a withdrawal.");
                return;
            }

            Console.WriteLine("Enter account number:");
            string? accountNumber = Console.ReadLine();
            if (string.IsNullOrEmpty(accountNumber))
            {
                Console.WriteLine("Account number cannot be empty.");
                return;
            }

            var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber && a.OwnerUserName == currentUser.Username);
            if (account == null)
            {
                Console.WriteLine("Account not found or access denied.");
                return;
            }

            Console.WriteLine("Enter amount to withdraw:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            if (!account.Withdraw(amount))
            {
                Console.WriteLine("Withdrawal failed. Insufficient funds.");
                return;
            }

            var withdrawalTransaction = new Transaction(accountNumber, "Withdrawal", -amount, DateTime.Now, "Withdrawal successful.");
            transactionLog.Add(withdrawalTransaction);

            NotifyCustomer($"Your account {accountNumber} has been debited by {amount:C}. New Balance: {account.Balance:C}");
            Console.WriteLine($"Withdrawal of {amount:C} successful!");
        }

        private void OpenNewAccount()
        {
            Console.Clear();
            Console.WriteLine("Enter Account Owner Name for new account:");
            string? accountOwnerName = Console.ReadLine();

            Console.WriteLine("Enter Password for new account:");
            string? password = Console.ReadLine();

            Console.WriteLine("Enter Account Number for new account:");
            string? accountNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(accountOwnerName) || string.IsNullOrEmpty(accountNumber))
            {
                Console.WriteLine("Invalid account owner name or account number. Account creation failed.");
                return;
            }

            Console.WriteLine("Enter Initial Balance:");
            if (decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
            {
                var newAccount = new BankAccount(accountOwnerName, accountNumber, initialBalance);
                accounts.Add(newAccount);
                Console.WriteLine("New account created successfully.");
            }
            else
            {
                Console.WriteLine("Invalid balance input. Account creation failed.");
            }
        }
        private void CheckLoanStatus()
        {
            Console.Clear();
            if (currentUser == null)
            {
                Console.WriteLine("No user is currently logged in.");
                return;
            }

            var userLoans = loans.Where(loan => loan.BorrowerUsername == currentUser.Username).ToList();

            if (!userLoans.Any())
            {
                Console.WriteLine("No loans found for your account.");
                return;
            }

            Console.WriteLine("Your Loan Status:");
            foreach (var loan in userLoans)
            {
                Console.WriteLine($"Loan Type: {loan.LoanType}, Amount: {loan.Amount:C}, Interest Rate: {loan.InterestRate}%, Repayment Months: {loan.RepaymentMonths}");
            }
        }
        private void RequestLoan()
        {
            try
            {
                Console.Write("Enter the loan amount: ");
                string? input = Console.ReadLine();

                if (!decimal.TryParse(input, out decimal loanAmount) || loanAmount <= 0)
                {
                    Console.WriteLine("Invalid loan amount. Please enter a positive number.");
                    return;
                }

                Console.WriteLine("Select loan type:");
                Console.WriteLine("1. Housing");
                Console.WriteLine("2. Vehicle");
                Console.WriteLine("3. Other");

                string? typeChoice = Console.ReadLine();

                string loanType = typeChoice switch
                {
                    "1" => "Housing",
                    "2" => "Vehicle",
                    "3" => "Other",
                    _ => throw new ArgumentException("Invalid loan type selected.")
                };

                // Proceed with loan request
                Console.WriteLine($"Loan request submitted for {loanType} loan of {loanAmount:C}.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred. Please try again.");
                Console.WriteLine($"Details: {ex.Message}");
            }
        }
        private void ViewTransactionHistory()
        {
            Console.Clear();
            Console.WriteLine("Transaction History:");
            if (currentUser == null)
            {
                Console.WriteLine("No user is currently logged in.");
                return;
            }

            var userAccounts = accounts.Where(account => account.OwnerUserName == currentUser.Username).Select(account => account.AccountNumber).ToList();
            var userTransactions = transactionLog.Where(transaction => userAccounts.Contains(transaction.FromAccount) || userAccounts.Contains(transaction.ToAccount));

            if (!userTransactions.Any())
            {
                Console.WriteLine("No transaction history available for your account.");
            }
            else
            {
                foreach (var transaction in userTransactions)
                {
                    Console.WriteLine($"{transaction.Date}: {transaction.Description} - {transaction.Amount:C}");
                }
            }
        }

        private void LogoutCustomer()
        {
            Console.Clear();
            Console.WriteLine("Logging out...");
            currentUser = null;  // Clear the current user to log them out
        }

        public decimal ConvertToUSD(decimal amount, string currency)
        {
            if (exchangeRates.ContainsKey(currency))
            {
                return amount / exchangeRates[currency];
            }
            else
            {
                throw new InvalidOperationException("Currency not supported for conversion.");
            }
        }

        private void TransferFunds()
        {
            Console.Clear();
            if (currentUser == null)
            {
                Console.WriteLine("No user is currently logged in.");
                return;
            }

            // List accounts owned by the current user
            var userAccounts = accounts.Where(account => account.OwnerUserName == currentUser.Username).ToList();
            if (userAccounts.Count < 2)
            {
                Console.WriteLine("You must have at least two accounts to transfer funds.");
                return;
            }

            Console.WriteLine("Your accounts:");
            for (int i = 0; i < userAccounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {userAccounts[i]}");
            }

            Console.WriteLine("Select the source account (number):");
            if (!int.TryParse(Console.ReadLine(), out int fromIndex) || fromIndex < 1 || fromIndex > userAccounts.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Console.WriteLine("Select the destination account (number):");
            if (!int.TryParse(Console.ReadLine(), out int toIndex) || toIndex < 1 || toIndex > userAccounts.Count || fromIndex == toIndex)
            {
                Console.WriteLine("Invalid selection or you selected the same account.");
                return;
            }

            Console.WriteLine("Enter the amount to transfer:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            var fromAccount = userAccounts[fromIndex - 1];
            var toAccount = userAccounts[toIndex - 1];

            if (fromAccount.TransferTo(toAccount, amount))
            {
                Console.WriteLine($"Transfer successful: {amount:C} from {fromAccount.AccountNumber} to {toAccount.AccountNumber}");
                var transaction = new Transaction(fromAccount.AccountNumber, toAccount.AccountNumber, amount, DateTime.Now, "Internal Transfer");
                transactionLog.Add(transaction);
            }
            else
            {
                Console.WriteLine("Transfer failed due to insufficient funds.");
            }
        }


        private void NotifyCustomer(string message)
        {
            if (currentUser != null)
            {
                Console.WriteLine($"Notification for {currentUser.Username}: {message}");
            }
        }

        // Scheduled Transactions Processing
        private void ProcessScheduledTransactions(object? state)
        {
            while (pendingTransactions.Count > 0)
            {
                var transaction = pendingTransactions.Dequeue();
                LogTransaction(transaction);
            }
        }

        // Log Transaction
        private void LogTransaction(Transaction transaction)
        {
            transactionLog.Add(transaction);
            Console.WriteLine($"Transaction logged: {transaction.Description} - {transaction.Amount:C}");
        }
        private void SaveUserData()
        {
            string filePath = "users.txt";
            var lines = users.Select(u => $"{u.Username}|{u.Password}|{u.IsAdmin}");
            File.WriteAllLines(filePath, lines);
            Console.WriteLine("User data saved successfully.");
        }

        private void LoadUserData()
        {
            string filePath = "users.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("User data file not found. Starting with default users.");
                SaveUserData();
                return; // If no file exists, do nothing
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 3)
                {
                    string username = parts[0];
                    string password = parts[1];
                    bool isAdmin = bool.Parse(parts[2]);

                    users.Add(new User(username, password, isAdmin));
                }
            }

            Console.WriteLine("User data loaded successfully.");
        }
        private string SelectLoanType()
        {
            Console.WriteLine("Available Loan Types:");
            Console.WriteLine("1. Housing Loan");
            Console.WriteLine("2. Vehicle Loan");
            Console.WriteLine("3. Personal Loan");
            Console.WriteLine("4. Other");
            Console.Write("Select a loan type (1-4): ");

            switch (Console.ReadLine())
            {
                case "1": return "Housing Loan";
                case "2": return "Vehicle Loan";
                case "3": return "Personal Loan";
                case "4": return "Other";
                default:
                    Console.WriteLine("Invalid selection. Try again.");
                    return SelectLoanType(); // Recursive call for retry
            }
        }

        private decimal ValidateLoanAmount()
        {
            Console.Write("Enter the loan amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                return amount;
            }

            Console.WriteLine("Invalid amount. Please enter a positive number.");
            return ValidateLoanAmount(); // Recursive call for retry
        }

    }
}
