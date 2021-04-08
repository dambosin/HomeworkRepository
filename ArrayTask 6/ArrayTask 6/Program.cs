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

            int uniques = 0;

            for (int i = 0; i < length; i++)
            {
                uniques += duplicateArr[i, 1] == 0? 1 : 0;
            }

            if (uniques > 0)
            {
                Console.WriteLine($"There are {uniques} uniques in this array such as");
                
                for(int i = 0; i < length; i++)
                {
                    if (duplicateArr[i, 1] == 0)
                    {
                        Console.WriteLine(duplicateArr[i, 0]);
                    }
                }
            }
            else
            {
                Console.WriteLine($"There are no uniques in this array");
            }
        }
    }
}
