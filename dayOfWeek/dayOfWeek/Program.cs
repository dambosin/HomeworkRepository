using System;

namespace dayOfWeek
{
    class Program
    {

        
        static void WriteDay(DayOfWeek day)
        {
            int temp = (int)day+1;
            Console.ForegroundColor = (ConsoleColor)temp;
            Console.WriteLine(day);
            Console.ResetColor();
        }

        static void WriteDays()
        {
            for(int i = 0; i < 7; i++)
            {
                WriteDay((DayOfWeek)i);
            }
        }

        
        static void Main(string[] args)
        {
            string day = Console.ReadLine();
            day = day.ToLower();
            bool isCorrect = true;
            DayOfWeek days = DayOfWeek.Monday;
            ConsoleColor dayColor = ConsoleColor.White;
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
}
