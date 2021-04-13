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
                WriteCommands();
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
                return TaskName + "," + TaskDate + "," + TaskPriority;
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
        public static WeeklyTask DisassemblyLine(string line)
        {
            var array = line.Split(',');
            switch (array.Length)
            {
                case 1:
                    return new WeeklyTask(array[0]);
                    break;
                case 2:
                    array[1] = array[1].Trim();
                    if (!DateTime.TryParse(array[1], out var dateTime))
                    {
                        throw new Exception("Cannot disassembly the date");
                    }
                    return new WeeklyTask(array[0],dateTime);
                    break;
                default:
                    array[1] = array[1].Trim();
                    if (!DateTime.TryParse(array[1], out var dateTime2))
                    {
                        throw new Exception("Cannot disassembly the date");
                    }
                    array[2] = array[2].Trim();
                    var priority = array[2].ToLower() switch
                        {
                            "low" => Priority.Low,
                            "medium" => Priority.Medium,
                            _ => Priority.High,
                        };
                        return new WeeklyTask(array[0], dateTime2, priority);
                    break;
            }
        }
        public static void WriteDataToFile(List<WeeklyTask> tasks)
        {
            WriteLine("Write the path to file");
            var path = ReadLine();
            using var writeFile = new StreamWriter(path, false, Encoding.Default);
            foreach (var i in tasks)
            {
                writeFile.WriteLine(i.WriteToFile());
            }
        }
        public static WeeklyTask AddTask()
        {
            WriteLine("Write task in format \n\"Name, Date Time, Priority\"\n\"Name, Date Time\"\n\"Name\"");
            return DisassemblyLine(ReadLine());
        }
        public static void DisplayTasks(List<WeeklyTask> tasks)
        {
            WriteLine("Do you want to see it in shor format?");
            var ans = ReadLine().ToLower();
            if(ans == "yes")
            {
                foreach (var i in tasks)
                {
                    i.DisplayTask(true);
                }
            }
            else
            {
                foreach (var i in tasks)
                {
                    i.DisplayTask();
                }
            }
        }
        public static void DisplayTasksWithFilter(List<WeeklyTask> tasks)
        {
            WriteLine("Enter filter in format \nFilter priority/date value");
            var ans = ReadLine().ToLower();
            var words = ans.Split(' ');
            if (words[1][0] == 'p')
            {
                DisplayTasksPriority(tasks, words[2]);
            }
            else
            {
                if (words.Length > 3)
                {
                    DisplayTasksDate(tasks, words[2] + ' ' + words[3]);
                }
                else
                {
                    DisplayTasksDate(tasks, words[2]);
                }
            }
        }
        public static void DisplayTasksDate(List<WeeklyTask> tasks, string word)
        {
            DateTime.TryParse(word, out DateTime date);
            foreach (var i in tasks)
            {
                if (i.TaskDate >= date)
                {
                    i.DisplayTask();
                }
            }
        }
        public static void DisplayTasksPriority(List<WeeklyTask> tasks, string word)
        {
            var priority = word switch
            {
                "low" => Priority.Low,
                "medium" => Priority.Medium,
                _ => Priority.High
            };
            foreach (var i in tasks)
            {
                if(i.TaskPriority == priority)
                {
                    i.DisplayTask();
                }
            }
        }
        public static void DisplayTasks(List<WeeklyTask> tasks, bool isShort)
        {
            foreach (var i in tasks)
            {
                i.DisplayTask(true);
            }
        }
        public static List<WeeklyTask> ChangeTaskData(List<WeeklyTask> tasks)
        {
            DisplayTasks(tasks, true);
            WriteLine("Write ID of task you want to change");
            int id;
            while(!int.TryParse(ReadLine(), out id))
            {
                WriteLine("Wrong enter");
            }
            var j = 0;
            for (int i = 0; i < tasks.Count; i++)
            {
                if(tasks[i].TaskID == id)
                {
                    j = i;
                }
            }
            WriteLine("1) Name");
            WriteLine("2) Date");
            WriteLine("3) Priority");
            ForegroundColor = ConsoleColor.DarkBlue;
            WriteLine("Which parametr you want to change");
            ResetColor();
            int.TryParse(ReadLine(), out int ans);
            switch (ans)
            {
                case 1:
                    WriteLine("Write new name");
                    tasks[j].TaskName = ReadLine();
                    break;
                case 2:
                    WriteLine("Write new date");
                    DateTime.TryParse(ReadLine(), out var date);
                    tasks[j].TaskDate = date;
                    tasks.Sort((a, b) => a.CompareTo(b));
                    break;
                default:
                    WriteLine("Write new priority");
                    var priority = ReadLine().ToLower();
                    switch (priority)
                    {
                        case "low":
                            tasks[j].TaskPriority = Priority.Low;
                            break;
                        case "medium":
                            tasks[j].TaskPriority = Priority.Medium;
                            break;
                        default:
                            tasks[j].TaskPriority = Priority.High;
                            break;
                    }
                    break;
            }
            return tasks;
        }
        public static void WriteCommands()
        {
            var tasks = new List<WeeklyTask>();
            while (true) {
                WriteLine("1) Read data from file");
                WriteLine("2) Store data to file");
                WriteLine("3) Add new task");
                WriteLine("4) Change task data");
                WriteLine("5) Show tasks");
                WriteLine("6) Show tasks with filter");
                ForegroundColor = ConsoleColor.DarkBlue;
                WriteLine("==> Choose the command <==");
                ResetColor();
                var answer = ReadLine();
                switch (answer.ToLower())
                {
                    case "1":
                        tasks = ReadDataFromFile();
                        break;
                    case "2":
                        WriteDataToFile(tasks);
                        break;
                    case "3":
                        tasks.Add(AddTask());
                        tasks.Sort((a, b) => a.CompareTo(b));
                        break;
                    case "4":
                        ChangeTaskData(tasks);
                        break;
                    case "5":
                        DisplayTasks(tasks);
                        break;
                    case "6":
                        DisplayTasksWithFilter(tasks);
                        break;
                    default:
                        WriteLine("Uncorrect enter");
                        break;
                }
            }
        }
    }
}
