using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static System.Console;
namespace Diary
{
    class Program
    {
        static void Main(string[] args)
        {

            var tasks = ReadDataFromFile();
            
           
            tasks.Sort((a, b) => a.CompareTo(b));
            foreach (var i in tasks)
            {
                i.DisplayTask(true);
            }
            
        }
        public enum Priority
        {
            Low = 10,
            Medium = 14,
            High = 12
        }
        public class WeeklyTask
        {
            public static int Counter;
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
            public string TaskName { get; set; }
            public DateTime TaskDate { get; set; } = DateTime.Today.AddDays(1);
            public Priority TaskPriority { get; set; } = Priority.Low;
            public int TaskID { get; set; }

            public WeeklyTask()
            {
                TaskID = Counter++;
                TaskName = "Task №" + TaskID;
            }
            public WeeklyTask(string name)
            {
                TaskID = Counter++;
                TaskName = name;
            }
            public WeeklyTask(string name, DateTime date)
            {
                TaskID = Counter++;
                TaskName = name;
                TaskDate = date;
            }
            public WeeklyTask(string name, DateTime date, Priority priority)
            {
                TaskID = Counter++;
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
        public static List<WeeklyTask> ReadDataFromFile()
        {
            WriteLine("Do you want to read data from file");
            var ans = ReadLine();
            if (ans.ToLower(default) == "yes")
            {
                WriteLine("Enter the path to file");
                var path = ReadLine();
                var tasks = new List<WeeklyTask>();
                using (var readFile = new StreamReader(path, Encoding.Default))
                {
                    string line;   
                    while ((line = readFile.ReadLine()) != null)
                    {
                        tasks.Add(DisassemblyLine(line));
                    }
                }
                return tasks;
            }
            else
            {
                return null;
            }
        }
        public static WeeklyTask DisassemblyLine(string line)
        {
            var array = line.Split(';');
            array[1] = array[1].Trim();
            if (DateTime.TryParse(array[1], out var dateTime))
            {
                array[2] = array[2].Trim();
                Priority priority;
                switch (array[2].ToLower())
                {
                    case "low":
                        priority = Priority.Low;
                        break;
                    case "medium":
                        priority = Priority.Medium;
                        break;
                    default:
                        priority = Priority.High;
                        break;
                }
                return new WeeklyTask(array[0], dateTime, priority);
            }
            else
            {
                throw new Exception("Cannot disassembly the line");
            }


        }
        public static string[] CheckFor0(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[i][0] == '0')
                {
                    arr[i] = arr[i].Remove(0, 1);
                }
            }
            return arr;
        }
    }
}
