# BankApp24
This is a C# Console app where the instructions were to make a console bank management program with a number of functionalities.

## Indiviual Project Work
 Eyosias Abera Mamo

# Bank Management System

A simple bank management system built in C#. This application allows users to manage bank accounts, perform transactions, and transfer funds using a command-line interface. It supports features such as user account creation, deposite fund, withdraw fund, loan service, notifications, currency conversion for transfers etc.
# Welcome page
![Skärmbild 2024-11-17 125945](https://github.com/user-attachments/assets/e143b61a-3fb9-4879-86be-8d268ae8f96d)
# Main Menu
![Skärmbild 2024-11-17 130012](https://github.com/user-attachments/assets/e381ba37-a770-45f3-ac38-d3f537ac4851)
# Admin Loging page
![Skärmbild 2024-11-17 130103](https://github.com/user-attachments/assets/7284119f-90b1-48b0-8d07-9e71233efa86)
# CustomerLoging 
![Skärmbild 2024-11-17 130138](https://github.com/user-attachments/assets/71326772-80e0-49e0-9e23-c1392dbb8173)

### OOAD (Open in new window):

https://github.com/eyosias87/BankAppProject/tree/master
---

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
- [Usage](#usage)
- [Main Menu](#main-menu-options)
  - [Logging In](#logging-in)
    - [Admin Menu Options](#customer-management)
      - Create New Account
      - Remove or Block Customer
      - View All User Accounts
      - View All Customer Loan Status
      - Update Customer Loan Interest Rates
      - Update Exchange Rates
      - Logout
    - [Customer Menu Options](#account-management)
      - View Account Summary
      - Deposit
      - Withdraw
      - Open New Account
      - Check Loan Status
      - Request Loan
      - Display Available Loan
      - View Transaction History
      - Logout
- [Validation and Security](#validation-and-security)
- [Contributing](#contributing)

---

## Features

- **Login System** (Admin Login, Customer Login and Exit)
- **Admin Account and Other Bank Service Management** (View Account, Create New Account, View All Customer Loan Status, Update Customer Loan Status, Update Customer Status, Update Exchange Rates and Logout from Admin Menu)
- **Customer Account Management** (View, Create, View, Deposit, Withdraw, Loan Status, Available Loan, Request Loan, View Transactions,Logout from Customer Menu)
- **Transaction Management and Display**
- **Currency Conversion for Transfers**
- **Admin and Customer Role Support**
- **Password Validation**

---

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (Version 8.0 or newer)

## Usage
### Main Menu Options
Choose to login as an admin or as a customer
### Logging In
Enter your username and password to log in to the bank system. 
---

### Admin Menu Options

After logging in, admin have access to several options.

- **Create New Account**: Allows you to create a new private or savings account..
- **Remove or Block Customer**: Allows the admin to remove or block a customer or customers.
- **View All User Accounts**: Able to see all customers accounts with details like name, account number, balace, currency etc.
- **View All Customer Loan Status**: ble to see customers that has loan with accounts details like name, account number, loan type, borrowed amount, repayment length of time in months, currency etc.
- **Uppdate Customer Loan Interest Rates**: Allows the admin to update specific customer loan interest rates.
- **Update Exchange Rates**: Lets an Admin account update the current exchange rate for all the currencies available.
- ****: 
- **Logout**: Ends admin's session and returns to the main menu where the user can login again.

### Create New Account
- Customer name
- Account number
- Saving account
### Remove or Block Customer
- Customer name
- Account number
### View All User Accounts
- Customer name
- Account number
- User status(admin or customer)
- Balance
- Currency
- Number of accounts
### View All Customer Loan Status
- Customer name
- Account number
- User status(admin or customer)
- Loan amount
- Currency
- Repayment length
### Uppdate Customer Loan Interest Rates
- Customer name
- Insert new interest rate
### Update Exchange Rates
- Insert new exchange rate

---
#### Currencies (Admin)
- Update the values of different available currencies.
- Add/Create new currencies to the program.

---

### ### Customer Menu Options
- **View Account Summary**: Allows you to see your balance in your account which you only have access to.
- **Deposit Funds**: Deposits money into your account or accounts which you have access to.
- **Withdraw Funds**: Withdraws money from one of your account or accounts which you have access to., subject to balance availability.
- **Open a New Account**: Allows you to open a new account.
- **Check Loan Status**: Allows you to see whether you have loan or not and gives you the details if you have loans from the bank.
- **Request Loan**: Allows you to choose type of loans, insert an amount and submit your request to the bank system.
- **Display Available Loans**: Allows you to choose type of loans.
- **View Transaction History**: Lists all past transactions with details.
- **Logout**: Ends your session and returns to the main menu where the user can login again.

### View Account Summary
- Name
- Account number
- Balance
- Currency
#### Deposit Funds
- The customer has only access to its account or accounts 
- Select an account to deposit to.
- Enter the amount to deposit.
- Choose currency type
#### Withdraw Funds
- The customer has only access to its account or accounts 
- Select an account to withdraw from.
- Enter an amount, ensuring it does not exceed the current balance.
#### Open a New Account
- Enter a name
- Account number
- Initial balance
- Select a currency (**USD**, **EUR**, **SEK**).
- Savings accounts come with a default interest rate.
### Check Loan Status
- Name
- Account
- Status
### Request Loan
- Choose loan types
- Amount of loan
### Display Available Loans
- Housing
- Vihecle
- Personal...
### View Transaction History
- Deposits
- Withdrawals
- Transfers
---

### Transfers

#### Transfer Within Your Own Accounts

- Select a source and destination account.
- Specify the amount to transfer.

#### Transfer to Another User's Account

- Enter the recipient's username.
- Select their account as the destination.
- Enter the amount to transfer.

---

## Validation and Security
- **Admin Validation**
- **Customer Validation**
- **Save User Data**
- **Load User Data**
- **Password Requirements**: At least 8 characters, including one uppercase letter, one lowercase letter, 
  and one digit.
- **Input Validation**: Ensures non-empty and correctly formatted input for all actions.
- **Transaction Validity**: Prevents overdrawing accounts and handles currency conversions for transfers.

---
