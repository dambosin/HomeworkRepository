using System;

namespace Dairy
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            WeeklyTaskService weeklyTaskService = new(@"D:\курсы\HomeworkRepository\Dairy\data.txt", Console.ReadLine, Console.WriteLine);

            weeklyTaskService.Loop();
        }
    }
}
