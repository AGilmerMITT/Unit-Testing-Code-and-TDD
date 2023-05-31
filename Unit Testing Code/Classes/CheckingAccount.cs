using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Testing_Code.Classes
{
    public class CheckingAccount
    {
        /*
         Test inputs:
        1. Standard (expected use)
        2. Boundary (edge cases, maximum/minimum)
        3. Incorrect (negative values, improper inputs)
        4. Explicit assumptions 
        5. Implicit assumptions
         */

        public double Balance { get; set; }

        public CheckingAccount(double balance = 0)
        {
            Balance = balance;
        }

        public void Withdraw(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (amount > Balance)
            {
                throw new InvalidOperationException();
            }

            Balance -= amount;
        }

        public void Deposit(double amount)
        {
            Balance += amount;
        }

        public void Transfer(CheckingAccount destinationAccount, double tranferBalance)
        {
            if (destinationAccount == null)
            {
                throw new ArgumentNullException(nameof(destinationAccount));
            }
            else if (destinationAccount.Balance + tranferBalance > double.MaxValue)
            {
                throw new InvalidOperationException("Destination account full");
            }
            else if (destinationAccount == this)
            {
                throw new InvalidOperationException("You can't send money to yourself");
            }

            this.Withdraw(tranferBalance);
            destinationAccount.Deposit(tranferBalance);
        }

        public void CloseAccount()
        {
            throw new NotImplementedException();
        }

        public void GetBalance()
        {
            throw new NotImplementedException();
        }
    }
}
