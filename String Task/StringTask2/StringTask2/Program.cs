using System;

namespace StringTask2
{
    class Program
    {
        static void Main(string[] args)
        {
            var someText = Console.ReadLine();
            var len = 0;
            foreach(char i in someText)
            {
                len++;
            }
            Console.WriteLine(len);
        }
    }
}
