namespace BankApp24
{
    public class NotificationService
    {
        private decimal transactionThreshold; // Threshold above which notifications are sent

        public NotificationService(decimal threshold)
        {
            transactionThreshold = threshold;
        }

        // Simulate sending an Email notification
        public void SendEmailNotification(string toEmail, string subject, string message)
        {
            // In a real-world application, you'd integrate with an email API like SendGrid or SMTP
            Console.WriteLine($"Email Sent to {toEmail}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }

        // Simulate sending an SMS notification
        public void SendSmsNotification(string phoneNumber, string message)
        {
            // In a real-world application, you'd integrate with a service like Twilio
            Console.WriteLine($"SMS Sent to {phoneNumber}");
            Console.WriteLine($"Message: {message}");
        }

        // Method to trigger notifications when a large transaction is detected
        public void NotifyLargeTransaction(Transaction transaction)
        {
            if (transaction.Amount > transactionThreshold)
            {
                // Example notification content
                string emailSubject = "Large Transaction Alert!";
                string emailMessage = $"A transaction of {transaction.Amount:C} has been made from account {transaction.FromAccount} to {transaction.ToAccount}.";
                string smsMessage = $"Alert: Large transaction of {transaction.Amount:C} from {transaction.FromAccount} to {transaction.ToAccount}.";

                // Simulating the notification being sent
                SendEmailNotification("customer@example.com", emailSubject, emailMessage);  // You can replace with real customer data
                SendSmsNotification("+1234567890", smsMessage);  // You can replace with real phone numbers
            }
        }
    }
}
