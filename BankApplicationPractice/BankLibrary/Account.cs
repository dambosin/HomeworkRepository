using System;

namespace BankLibrary
{
    public delegate void AccountCreated(string message);
    
    public abstract class Account
    {
        private static int _counter = 0;
        private decimal _amount;
        private int _id;
        private int _days = 0;
        private AccountState _state;

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

        public event AccountCreated Created;

        public Account(decimal amount)
        {
            _amount = amount;
            _state = AccountState.Created;
            _id = ++_counter;
        }

        public virtual void Open()
        {
            AssertValidState(AccountState.Created);

            _state = AccountState.Opened;
            IncrementDays();
            Created?.Invoke("Account created.");
        }
        
        public virtual void Close()
        {
            AssertValidState(AccountState.Opened);
    
            _state = AccountState.Closed;
        }
        
        public virtual void Put(decimal amount)
        {
            AssertValidState(AccountState.Opened);

            _amount += amount;
        }
        
        public virtual void Withdraw(decimal amount)
        {
            AssertValidState(AccountState.Opened);

            if (_amount < amount)
            {
                throw new InvalidOperationException("Not enough money");
            }

            _amount -= amount;
        }
        
        public abstract AccountType Type { get; }

        private void AssertValidState(AccountState validState)
        {
            if (_state != validState)
            {
                throw new InvalidOperationException($"Invalid account state: {_state}");
            }
        }

        protected int Days => _days;
        public int Id => _id;

        public void IncrementDays()
        {
            if (_state == AccountState.Opened)
            {
                _days++;
                _amount += _amount / 1000;
            }
        }
    }
}