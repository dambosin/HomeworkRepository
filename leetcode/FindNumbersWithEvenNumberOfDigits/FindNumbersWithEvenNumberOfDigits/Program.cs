using System;

namespace FindNumbersWithEvenNumberOfDigits
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = Convert.ToInt32(Console.ReadLine());
            int x;
            var amoutEven = 0;
            
            for(int i = 0; i < n; i++)
            {
                var isEven = true;
                x = Convert.ToInt32(Console.ReadLine());

                while (x > 0)
                {
                    x /= 10;
                    isEven = !isEven; 
                }

                amoutEven += isEven ? 1 : 0;
            }

            Console.WriteLine(amoutEven);
        }
    }
}
