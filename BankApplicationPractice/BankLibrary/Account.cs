using System;

namespace BankLibrary
{
    public delegate void AccountNotification(string message);
    
    public abstract class Account
    {
        private static int s_counter;
        private decimal _amount;
        protected int Days { get; private set; }
        protected int Id { get; }
        private AccountState _state;
        public abstract AccountType Type { get; }
        public event AccountNotification Notify;


        public override string ToString()
        {
            if (_state != AccountState.Closed)
            {
                return $"{Id}. {Type} {_amount} money, {Days} days";
            }
            else
            {
                return $"{Id}. {Type} {_state}";
            }
        }

        public Account(decimal amount)
        {
            _amount = amount;
            _state = AccountState.Created;
            Id = ++s_counter;
        }

        public virtual void Open()
        {
            AssertValidState(AccountState.Created);

            _state = AccountState.Opened;
            Notify?.Invoke("Account created.");
        }
        
        public virtual void Close()
        {
            AssertValidState(AccountState.Opened);
    
            _state = AccountState.Closed;

            Notify?.Invoke("Account closed.");
        }
        
        public virtual void Put(decimal amount)
        {
            AssertValidState(AccountState.Opened);

            _amount += amount;
            Notify?.Invoke($"{amount} was added on account.");
        }
        
        public virtual void Withdraw(decimal amount)
        {
            AssertValidState(AccountState.Opened);
            AssertValidAmount(amount);
            _amount -= amount;
            Notify?.Invoke($"{amount} was withdrawed from account");
        }

        private void AssertValidAmount(decimal amount)
        {
            if (_amount < amount)
            {
                throw new InvalidOperationException("Not enough money");
            }
        }

        private void AssertValidState(AccountState validState)
        {
            if (_state != validState)
            {
                throw new InvalidOperationException($"Invalid account state: {_state}");
            }
        }

        public void IncrementDays()
        {
            if (_state == AccountState.Opened)
            {
                Days++;
                _amount = CalculatePercentages(_amount);
            }
        }

        internal abstract decimal CalculatePercentages(decimal amount);

    }
}
