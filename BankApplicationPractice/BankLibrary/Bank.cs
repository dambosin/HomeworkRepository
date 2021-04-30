using System;
using System.Collections.Generic;

namespace BankLibrary
{

    public class Locker
    {
        private object _data;
        private string _keyword;
        private int _id;

        public Locker(int id, string keyword, object data)
        {
            _id = id;
            _keyword = keyword;
            _data = data;
        }
        public int Id => _id;
        public object Data => _data;
        public bool Matches(int id,string keyword)
        {
            return id == _id && keyword.Equals(_keyword, StringComparison.Ordinal);
        }
    }
    public class Bank<T> where T : Account
    {
        private List<Locker> _lockers = new();
        public Action<string> _writeOutput;

        private readonly List<T> _accounts = new();

        public Bank(Action<string> writeOutput){
            _writeOutput = writeOutput;
        }

        public int  AddLocker(string keyword, object data)
        {

            var locker = new Locker(_lockers.Count + 1, keyword, data);
            _lockers.Add(locker);
            return locker.Id;
        }

        public object GetLockerData(int id, string keyword)
        {

            foreach (var locker in _lockers)
            {
                if(locker.Matches(id, keyword))
                {
                    return locker.Data;
                }
            }
            throw new ArgumentOutOfRangeException($"Cannot find locker with id {id} or keyword does not match");
        }

        public TU GetLockerData<TU>(int id, string keyword)
        {

            return (TU)GetLockerData(id, keyword);
        }

        public void OpenAccount(OpenAccountParameters parameters)
        {
            AssertValidType(parameters.Type);
            CreateAccount(parameters.AccountNotify, () => parameters.Type == AccountType.Deposit 
                ? new DepositAccount(parameters.Amount) as T
                : new OnDemandAccount(parameters.Amount) as T);
        }

        private static void AssertValidType(AccountType type)
        {
            var itemType = typeof(T);
            if(itemType != typeof(Account) && ((type == AccountType.Deposit && itemType != typeof(DepositAccount)) 
                                           || (type == AccountType.OnDemand && itemType != typeof(OnDemandAccount))))
            {
                throw new InvalidOperationException("Ivalid account type.");
            }

        }

        private void CreateAccount(AccountNotification accountNotify, Func<T> creator)
        {
            var account = creator();
            account.Notify += accountNotify;
            account.Open();
            
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
