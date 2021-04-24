using BankLibrary;
using System;


namespace BankApplication
{
    public class Program
    {
        private static readonly Bank<Account> s_bank1 = new(Write);
        
        public static void Main(string[] args)
        {
            var alive = true;
            while (alive)
            {
                var  color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("1. Open Account \t 2. Withdraw sum \t 3. Add sum");
                Console.WriteLine("4. Close Account \t 5. Skip day \t 6. Exit program");
                Console.WriteLine("Enter the item number:");
                Console.ForegroundColor = color;
                try
                {
                    var choice = Convert.ToInt32(Console.ReadLine(), (IFormatProvider)default);

                    Console.Clear();

                    switch (choice)
                    {
                        case 1:
                            OpenAccount();
                            break;
                        case 2:
                            Withdraw();
                            break;
                        case 3:
                            Put();
                            break;
                        case 4:
                            CloseAccount();
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    CalculatePercentage();
                }
                catch (Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
                DisplayAccounts();
            }
        }

        private static void DisplayAccounts()
        {
            s_bank1.DisplayAccounts();
        }

        public static void Write(string text)
        {
            Console.WriteLine(text);
        }

        private static void CalculatePercentage()
        {
            s_bank1.IncrementDays();
        }

        private static void OpenAccount()
        {
            Console.WriteLine("Specify the sum to create an account: ");
            var sum = Convert.ToDecimal(Console.ReadLine(), default);

            Console.WriteLine("Select an account type: \n 1. On-Demand \n 2. Deposit");
            var type = Enum.Parse<AccountType>(Console.ReadLine()!);
            
            s_bank1.OpenAccount(new OpenAccountParameters
            {
                Amount = sum,
                Type = type,
                AccountCreated = NotifyAccountCreated
            });
        }

        private static void Withdraw()
        {
            Console.WriteLine("Specify the sum to withdraw from the account: ");
            var sum = Convert.ToDecimal(Console.ReadLine(), default);

            Console.WriteLine("Enter account id: ");
            var id = Convert.ToInt32(Console.ReadLine(), (IFormatProvider)default);

            s_bank1.Withdraw(id, sum);
        }

        private static void Put()
        {
            Console.WriteLine("Specify the sum to put on the account: ");
            var sum = Convert.ToDecimal(Console.ReadLine(), default);

            Console.WriteLine("Enter account id: ");
            var id = Convert.ToInt32(Console.ReadLine(), (IFormatProvider)default);

            s_bank1.Put(id, sum);

        }
        
        private static void CloseAccount()
        {
            Console.WriteLine("Enter the account id to close: ");
            var id = Convert.ToInt32(Console.ReadLine(), (IFormatProvider)default);

            s_bank1.CloseAccount(id);
        }

        private static void NotifyAccountCreated(string message)
        {
            Console.WriteLine(message);
        }
    }
}
