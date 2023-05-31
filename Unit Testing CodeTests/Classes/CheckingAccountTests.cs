using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit_Testing_Code.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Testing_Code.Classes.Tests
{
    [TestClass()]
    public class CheckingAccountTests
    {
        [TestMethod()]
        public void TestWithdrawValidAmount()
        {
            // Three A's approach

            // Arrange
            // get all our variables together
            // prepare for the test
            double initialBalance = 10.0;
            double withdrawAmount = 1.0;
            double finalBalance = 9.0;

            // Act
            // Run the test.  DO THE THING. 
            var account = new CheckingAccount(initialBalance);
            account.Withdraw(withdrawAmount);

            // Assert
            // Declare the result of the test
            Assert.AreEqual(finalBalance, account.Balance);
        }

        [TestMethod()]
        public void TestWithdrawOverdrawAmount()
        {
            // AAA
            // Arrange
            double initialBalance = 10.0;
            double withdrawAmount = 15.0;

            // Act & Assert
            var account = new CheckingAccount(initialBalance);
            Assert.ThrowsException<InvalidOperationException>(
                () => account.Withdraw(withdrawAmount)
                );
        }

        [TestMethod()]
        public void TestWithdrawNegativeAmount()
        {
            // AAA
            double initialBalance = 10.0;
            double withdrawAmount = -5.0;

            // Act and Assert
            var account = new CheckingAccount(initialBalance);
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => account.Withdraw(withdrawAmount)
                );
        }

        [TestMethod()]
        public void TestAccountInitialization()
        {
            // AAA
            // Arrange
            double initialBalance = 50.0;

            // Act
            var account = new CheckingAccount(initialBalance);

            // Assert
            Assert.AreEqual(initialBalance, account.Balance);
        }

        [TestMethod()]
        public void TestAccountTransferSourceLostMoney()
        {
            // as a user, i want to be able to transfer money to other people\
            // a user needs to have an account to transfer money
            // the money needs to leave that users account
            // and then go to the other user's account

            // AAA
            double initialBalance = 10.0;
            double tranferBalance = 5.0;
            double expectedRemainingBalance = 5.0;

            var sourceAccount = new CheckingAccount(initialBalance);
            var destinationAccount = new CheckingAccount();

            // Act
            sourceAccount.Transfer(destinationAccount, tranferBalance);

            // Assert
            Assert.AreEqual(sourceAccount.Balance, expectedRemainingBalance);
        }

        [TestMethod()]
        public void TestAccountTransferDestinationGainedMoney()
        {
            // AAA
            // Arrange
            double initialBalance = 10.0;
            double tranferBalance = 5.0;
            double expectedDestinationBalance = 5.0;

            var sourceAccount = new CheckingAccount(initialBalance);
            var destinationAccount = new CheckingAccount();

            // Act
            sourceAccount.Transfer(destinationAccount, tranferBalance);

            // Assert
            Assert.AreEqual(destinationAccount.Balance, expectedDestinationBalance);
        }

        [TestMethod()]
        public void TestAccountTransferMoneyWithdrawFailsBecauseOverdrawButDepositSucceeds()
        {
            // AAA
            double initialBalance = 5.0;
            double tranferBalance = 10.0;
            double expectedDestinationBalance = 0;

            var sourceAccount = new CheckingAccount(initialBalance);
            var destinationAccount = new CheckingAccount();

            // Assert
            Assert.ThrowsException<InvalidOperationException>(
                () => sourceAccount.Transfer(destinationAccount, tranferBalance)
                );
        }

        [TestMethod()]
        public void TestAccountTransferAccountExistsCheck()
        {
            // AAA
            // Arrange
            double transferAmount = 0;
            CheckingAccount sourceAccount = new CheckingAccount();
            CheckingAccount destinationAccount = null;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => sourceAccount.Transfer(destinationAccount, transferAmount)
                );
        }

        [TestMethod()]
        public void TestAccountTransferMoneyToSelf()
        {
            // AAA
            double transferAmount = 5.0;
            double initialBalance = 5.0;

            CheckingAccount sourceAccount = new CheckingAccount(initialBalance);

            // Act/Assert
            Assert.ThrowsException<InvalidOperationException>(
                () => sourceAccount.Transfer(sourceAccount, transferAmount)
                );
        }

        [TestMethod()]
        public void TestAccountTransferDestinationFull()
        {
            // AAA
            double transferAmount = 1e300;
            double sourceBalance = 1e300;
            double destinationBalance = double.MaxValue;
            var sourceAccount = new CheckingAccount(sourceBalance);
            var destinationAccount = new CheckingAccount(destinationBalance);

            // Act/Assert
            Assert.ThrowsException<InvalidOperationException>(
                () => sourceAccount.Transfer(destinationAccount, transferAmount)
                );
        }

        [TestMethod()]
        public void TestClosedAccountIsInaccessible()
        {
            // AAA
            // Arrange
            var account = new CheckingAccount();
            account.CloseAccount();

            // Act/Assert
            Assert.ThrowsException<InvalidOperationException>(
                () => account.GetBalance()
                );
        }

        [TestMethod()]
        public void TestClosingAClosedAccount()
        {
            // AAA
            // Arrange
            var account = new CheckingAccount();
            account.CloseAccount();

            // Act/Assert
            Assert.ThrowsException<InvalidOperationException>(
                () => account.CloseAccount()
                );
        }

        [TestMethod()]
        public void TestWithdrawFromClosedAccount()
        {
            // Arrange
            double initialBalance = 15.0;
            double withdrawAmount = 10.0;
            var account = new CheckingAccount(initialBalance);
            account.CloseAccount();

            Assert.ThrowsException<InvalidOperationException>(
                () => account.Withdraw(withdrawAmount)
                );
        }

        [TestMethod()]
        public void TestTransferMoneyToClosedAccount()
        {
            double sourceBalance = 25.0;
            double destinationBalance = 30.0;
            var sourceAccount = new CheckingAccount(sourceBalance);
            var destinationAccount = new CheckingAccount(destinationBalance);
            destinationAccount.CloseAccount();
            double transferAmount = 10.0;

            // Act/Assert
            Assert.ThrowsException<InvalidOperationException>(
                () => sourceAccount.Transfer(destinationAccount, transferAmount)
                );
            Assert.AreEqual(sourceAccount.Balance, sourceBalance);
            Assert.AreEqual(destinationAccount.Balance, destinationBalance);
        }
    }
}