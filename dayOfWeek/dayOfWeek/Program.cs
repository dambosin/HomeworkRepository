using System;

namespace dayOfWeek
{
    class Program
    {
        static void Main(string[] args)
        {
            string day = Console.ReadLine();
            day = day.ToLower();
            bool isCorrect = true;
            DayOfWeek days = DayOfWeek.Monday;
            switch(day)
            {
                case "monday": case "mon":
                    days = DayOfWeek.Monday;
                    break;
                case "tuesday":
                case "tue":
                    days = DayOfWeek.Tuesday;
                    break;
                case "wednesday":
                case "wed":
                    days = DayOfWeek.Wednesday;
                    break;
                case "thursday":
                case "thu":
                    days = DayOfWeek.Thursday;
                    break;
                case "friday":
                case "fri":
                    days = DayOfWeek.Friday;
                    break;
                case "saturday":
                case "sat":
                    days = DayOfWeek.Saturday;
                    break;
                case "sunday":
                case "sun":
                    days = DayOfWeek.Sunday;
                    break;
                default:
                    Console.WriteLine("There is no such day in our week");
                    isCorrect = false;
                    break;
            }
            if (isCorrect)
            {
                Console.WriteLine(days);
            }
            else
            {
                Console.WriteLine("There is no such day in week");
            }
        }
    }
}
