using System;
using System.Collections.Generic;
namespace Diary
{
    class Program
    {
        static void Main(string[] args)
        {
            List<WeeklyTask> tasks = new List<WeeklyTask>();
            tasks.Add(new WeeklyTask());
            tasks.Add(new WeeklyTask("hey",DateTime.Now.AddHours(2),Priority.High));
            tasks.Add(new WeeklyTask("bye",DateTime.Now,Priority.Medium));

            tasks.Sort((a, b) => a.CompareTo(b));
            foreach (var i in tasks)
            {
                i.DisplayTask(true);
            }

        }
    }
    enum Priority
    {
        Low = 10,
        Medium = 14,
        High = 12
    }
    class WeeklyTask
    {   
        public static int counter = 0;
        public static bool operator >(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate > task2.TaskDate;
        public static bool operator <(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate < task2.TaskDate;
        public static bool operator ==(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate == task2.TaskDate;
        public static bool operator !=(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate != task2.TaskDate;
        public static bool operator >=(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate >= task2.TaskDate;
        public static bool operator <=(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate <= task2.TaskDate;
        public int CompareTo(WeeklyTask obj)
        {
            if (this > obj) return 1;
            if (this == obj) return 0;
            if (this < obj) return -1;
            return 0;
        }
        public override string ToString()
        {
            return "ID " + TaskID + "; " + TaskName + "; " + TaskDate + "; " + TaskPriority + ";";
        }
        public string TaskName{ get; private set; }
        public DateTime TaskDate { get; private set; } = DateTime.Today.AddDays(1);
        public Priority TaskPriority { get; private set; } = Priority.Low;
        public int TaskID { get; private set; }

        public WeeklyTask()
        {
            TaskID = counter++;
            TaskName = "Task №" + TaskID;
        }
        public WeeklyTask(string name)
        {
            TaskID = counter++;
            TaskName = name;
        }
        public WeeklyTask(string name, DateTime date)
        {
            TaskID = counter++;
            TaskName = name;
            TaskDate = date;
        }
        public WeeklyTask(string name, DateTime date, Priority priority)
        {
            TaskID = counter++;
            TaskName = name;
            TaskDate = date;
            TaskPriority = priority;
        }

        public void DisplayTask()
        {
            Console.ForegroundColor = (ConsoleColor)TaskPriority;
            Console.WriteLine($"ID {TaskID}");
            Console.WriteLine(TaskName);
            Console.WriteLine(TaskDate);
            Console.WriteLine($"Priority is {TaskPriority}\n");
            Console.ResetColor();
        }

        public void DisplayTask(bool isShort)
        {
            if (isShort) 
            {
                Console.ForegroundColor = (ConsoleColor)TaskPriority;
                Console.WriteLine(this);
                Console.ResetColor();
            }
            else
            {
                DisplayTask();
            }
        }

    }



}
