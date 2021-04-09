using System;

namespace StringTask3
{
    class Program
    {
        static void Main(string[] args)
        {
            var someText = Console.ReadLine();
            char[] arr = someText.ToCharArray();

            foreach(char i in arr)
            {
                Console.Write($"{i} ");
            }
        }
    }
}
