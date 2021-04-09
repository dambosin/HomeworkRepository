using System;

namespace StringTask3
{
    class Program
    {
        static void Main(string[] args)
        {
            var someText = Console.ReadLine();
            
            for(int i = 0; i < someText.Length; i += 2)
            {
                someText = someText.Insert(i + 1, " ");
            }

            Console.WriteLine(someText);
        }
    }
}
