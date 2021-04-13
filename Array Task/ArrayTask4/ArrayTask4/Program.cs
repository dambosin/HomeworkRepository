using System;

namespace ArrayTask4
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[] arr1 = new int[n];

            for(int i = 0; i < n; i++)
            {
                arr1[i] = Convert.ToInt32(Console.ReadLine());
            }
            
            int[] arr2 = new int[arr1.Length];
            arr1.CopyTo(arr2, 0);
            Console.WriteLine("Firs array");
            WriteArray(arr1);
            Console.WriteLine("Copied array");
            WriteArray(arr2);
        }
        static void WriteArray(int[] arr)
        {
            foreach(int i in arr)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
        }
    }
}
