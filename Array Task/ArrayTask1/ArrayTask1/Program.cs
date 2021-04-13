using System;

namespace ArrayTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[10];

            for(int i = 0; i < 10; i++)
            {
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }

            for(int i = 0; i < 10; i++)
            {
                Console.Write($"{arr[i]} ");
            }
        }
    }
}
