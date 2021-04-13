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
            WriteDataToFile(tasks);
            
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
            public string WriteToFile()
            {
                return TaskName + ";" + TaskDate + ";" + TaskPriority;
            }
            public void DisplayTask()
            {
                ForegroundColor = (ConsoleColor)TaskPriority;
                WriteLine($"ID {TaskID}");
                WriteLine(TaskName);
                WriteLine(TaskDate);
                WriteLine($"Priority is {TaskPriority}\n");
                ResetColor();
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
            WriteLine("Do you want to read data from file?");
            var ans = ReadLine().ToLower();
            if (ans.ToLower(default) == "yes")
            {
                WriteLine("Write the path to file");
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
            if (!DateTime.TryParse(array[1], out var dateTime))
            {
                throw new Exception("Cannot disassembly the date");
            }
            var priority = array[2].ToLower() switch
            {
                "low" => Priority.Low,
                "medium" => Priority.Medium,
                _ => Priority.High,
            };
            return new WeeklyTask(array[0], dateTime, priority);


        }
        public static void WriteDataToFile(List<WeeklyTask> tasks)
        {
            WriteLine("Do you want to store data in file?");
            var ans = ReadLine().ToLower();
            if(ans == "yes")
            {
                WriteLine("Write the path to file");
                var path = ReadLine();
                using (var writeFile = new StreamWriter(path, false, Encoding.Default))
                {
                    foreach(WeeklyTask i in tasks)
                    {
                        writeFile.WriteLine(i.WriteToFile());
                    }
                }
            }
        }
    }
}
