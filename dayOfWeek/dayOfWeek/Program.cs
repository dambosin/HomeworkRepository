using System;

namespace dayOfWeek
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string day = Console.ReadLine();
                bool isCorrect = true;
                DayOfWeek days = DayOfWeek.Monday;

                switch (day.ToLower())
                {
                    case "monday" or "mon":
                        break;
                    case "tuesday" or "tue":
                        days = DayOfWeek.Tuesday;
                        break;
                    case "wednesday" or "wed":
                        days = DayOfWeek.Wednesday;
                        break;
                    case "thursday" or "thu":
                        days = DayOfWeek.Thursday;
                        break;
                    case "friday" or "fri":
                        days = DayOfWeek.Friday;
                        break;
                    case "saturday" or "sat":
                        days = DayOfWeek.Saturday;
                        break;
                    case "sunday" or "sun":
                        days = DayOfWeek.Sunday;
                        break;
                    default:
                        isCorrect = false;
                        break;
                }

                if (isCorrect)
                {
                    WriteDay(days);
                }
                else
                {
                    Console.WriteLine("There is no such day in week");
                }
            }
        }
        static void WriteDay(DayOfWeek day)
        {
            var temp = day + 1;
            var todayDay = DateTime.Now.DayOfWeek;

            if (todayDay == day)
            {
                Console.Write("Today is ");
            }

            Console.ForegroundColor = (ConsoleColor)temp;
            Console.WriteLine($"{day} - {(int)day}");
            Console.ResetColor();

            if ((int)day != 6 && day != 0)
            {
                Console.WriteLine($"{6 - day} days till weekends");
            }
            else
            {
                Console.WriteLine($"You are on the weekends!");
            }

            int daysTillNext = todayDay >= day ? day + 7 - todayDay : day - todayDay;
            Console.Write($"The next {day} will be on ");
            Console.WriteLine(DateTime.Now.AddDays(daysTillNext).ToShortDateString());
        }
    }
}
