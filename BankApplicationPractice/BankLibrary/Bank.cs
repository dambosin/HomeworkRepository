using System;
using System.Collections.Generic;

namespace BankLibrary
{
     public delegate void WriteOutput(string line);
    public class Bank<T> where T : Account
    {
        private WriteOutput _writeOutput;
        private readonly List<T> _accounts = new();

        public Bank(WriteOutput writeOutput){
            _writeOutput = writeOutput;
        }
        public void OpenAccount(OpenAccountParameters parameters)
        {
            AssertValidType(parameters.Type);
            CreateAccount(parameters.AccountCreated, () => parameters.Type == AccountType.Deposit 
                ? new DepositAccount(parameters.Amount) as T
                : new OnDemandAccount(parameters.Amount) as T);
        }

        private void AssertValidType(AccountType type)
        {
            Type itemType = typeof(T);
            if(itemType != typeof(Account) && ((type == AccountType.Deposit && itemType != typeof(DepositAccount)) 
                                           || (type == AccountType.OnDemand && itemType != typeof(OnDemandAccount))))
            {
                throw new InvalidOperationException("Ivalid account type.");
            }

        }

        private void CreateAccount(AccountCreated accountCreated, Func<T> creator)
        {
            var account = creator();
            account.Open();
            account.Created += accountCreated;
            _accounts.Add(account);
        }
        public void Withdraw(int id, decimal amount)
        {
            AssertValidId(id--);
            _accounts[id].Withdraw(amount);
        }

        private void AssertValidId(int id)
        {
            if (id < 1 || id > _accounts.Count)
            {
                throw new InvalidOperationException("Not valid Id");
            }
        }

        public void Put(int id, decimal amount)
        {
            AssertValidId(id--);
            _accounts[id].Put(amount);
        }

        public void CloseAccount(int id)
        {
            AssertValidId(id--);
            _accounts[id].Close();
        }

        public void DisplayAccounts()
        {
            foreach (var item in _accounts)
            {
                _writeOutput?.Invoke($"{item}");
            }
        }
        public void IncrementDays()
        {
            foreach(var item in _accounts)
            { 
                item.IncrementDays();
            }
        }
    }
}