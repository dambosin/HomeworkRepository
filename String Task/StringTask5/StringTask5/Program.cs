using System;

namespace StringTask5
{
    class Program
    {
        static void Main(string[] args)
        {
            var someText = Console.ReadLine().ToLower();
            var amount = 0;
            bool isWord = false;
            
            for(int i = 0; i < someText.Length; i++)
            {
                if (someText[i] != ' ' & !isWord)
                {
                    amount++;
                    isWord = true;
                }

                if(someText[i] == ' ')
                {
                    isWord = false;
                }
            }

            Console.WriteLine(amount);
        }
    }
}
