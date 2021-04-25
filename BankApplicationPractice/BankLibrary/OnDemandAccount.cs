namespace BankLibrary
{
    public class OnDemandAccount : Account
    {
        public OnDemandAccount(decimal amount) 
            : base(amount)
        {
        }

        public override AccountType Type => AccountType.OnDemand;

        internal override decimal CalculatePercentages(decimal amount)
        {
            return amount / 20 + amount;
        }
    }
}
