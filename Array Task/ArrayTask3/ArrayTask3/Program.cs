using System;

namespace ArrayTask3
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            long sum = 0;

            for(int i = 0; i < n; i++)
            {
                sum += Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine($"Sum of the array equals to {sum}");
        }
    }
}
