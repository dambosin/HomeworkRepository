using System;

namespace StringTask4
{
    class Program
    {
        static void Main(string[] args)
        {
            var someText = Console.ReadLine();
            char[] arr = someText.ToCharArray();

            for (int i = 0; i< arr.Length; i++)
            {
                Console.Write($"{arr[arr.Length - i - 1]} ");
            }
        }
    }
}
