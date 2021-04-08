using System;

namespace ArrayTask_5
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[n];
            int[,] duplicateArr = new int[n, 2];
            int length = 0;

            for (int i = 0; i < n; i++)
            {
                arr[i] = Convert.ToInt32(Console.ReadLine());
                bool isUnique = true;
                int j = 0;

                while (isUnique && j <= length)
                {
                    isUnique = arr[i] != duplicateArr[j++, 0];
                }

                if (isUnique)
                {
                    duplicateArr[length, 0] = arr[i];
                    duplicateArr[length++, 1] = 0;
                }
                else
                {
                    duplicateArr[--j, 1]++;
                }
            }

            int duplicates = 0;

            for (int i = 0; i <= duplicateArr.GetUpperBound(0); i++)
            {
                duplicates += duplicateArr[i, 1];
            }

            Console.WriteLine($"There are {duplicates} duplicates in this array");
        }
    }
}
