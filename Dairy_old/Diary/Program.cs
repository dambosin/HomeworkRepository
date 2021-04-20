using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static System.Console;
namespace Diary
{
    public class Program
    {
        public static void Main()
        {
            if (WelcomeToProgramm())
            {
                Start();
            }
            else
            {
                WriteLine("Then why did you open this programm?");
            }
        }
        public static bool WelcomeToProgramm()
        {
            WriteLine("Hey it is my dairy programm.\nIt has an input check but I will appreceate if you will not try to breake it.\nDo you want to Start?");
            var userInput = ReadLine().ToLower(System.Globalization.CultureInfo.CurrentCulture);
            return userInput switch
            {
                "yes" => true,
                _ => false,
            };
        }
        public enum Priority
        {
            Low = 10,
            Medium = 6,
            High = 12
        }
        public static void Start()
        {
            var taskList = new List<WeeklyTask>();
            while (true)
            {
                WriteCommandsExample();
                int userInput;
                while (!int.TryParse(ReadLine(), out userInput))
                {
                    IntTryParseError();
                }
                ChooseCommandExample(taskList, userInput);
            }
        }
        public static void WriteCommandsExample()
        {
            ConsoleWrite("1) Read data from file");
            ConsoleWrite("2) Store data to file");
            ConsoleWrite("3) Add new task");
            ConsoleWrite("4) Change task data");
            ConsoleWrite("5) Show tasks");
            ConsoleWrite("6) Show tasks with filter");
            ConsoleWrite("==> Choose the command <==");
        }
        private static void ChooseCommandExample(List<WeeklyTask> taskList, int userInput)
        {
            switch (userInput)
            {
                case 1:
                    taskList = ReadDataFromFile(taskList);
                    break;
                case 2:
                    WriteDataToFile(taskList);
                    break;
                case 3:
                    taskList.Add(AddTask());
                    taskList.Sort((a, b) => a.CompareTo(b));
                    break;
                case 4:
                    ChangeTaskData(taskList);
                    taskList.Sort((a, b) => a.CompareTo(b));
                    break;
                case 5:
                    DisplayTasks(taskList);
                    break;
                case 6:
                    DisplayTasksWithFilter(taskList);
                    break;
                default:
                    ConsoleWrite("From 1 to 6 ...");
                    break;
            }
        }
        public static List<WeeklyTask> ReadDataFromFile(List<WeeklyTask> taskList)
        {
            var path = GetPath();
            using var readFile = new StreamReader(path, Encoding.Default);
            string line;   
            while ((line = readFile.ReadLine()) != null)
            {
                taskList.Add(DisassemblyLine(line));
            }
            return taskList;
        }
        public static WeeklyTask DisassemblyLine(string line)
        {
            var array = line.Split(',');
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = array[i].Trim();
            }
            return array.Length switch
            {
                1 => new WeeklyTask(array[0]),
                2 => new WeeklyTask(array[0], StringToDate(array[1])),
                _ => new WeeklyTask(array[0], StringToDate(array[1]), StringToPriority(array[2])),
            };
        }
        public static DateTime StringToDate(string dateString)
        {
            DateTime date;
            while(!DateTime.TryParse(dateString, out date))
            {
                DateTryParseError();
                dateString = ReadLine();
            }
            return date;
        }
        public static Priority StringToPriority(string priorityString)
        {
            while (true)
            {
                switch (priorityString.ToLower(System.Globalization.CultureInfo.CurrentCulture))
                {
                    case "low":
                        return Priority.Low;
                    case "medium":
                        return Priority.Medium;
                    case "high":
                        return Priority.High;
                    default:
                        PriorityTryParseError();
                        priorityString = ReadLine();
                        break;
                }
            }
        }
        public static void DateTryParseError() => WriteLine("Incorrect enter of date. Please try again");
        public static void PriorityTryParseError() => WriteLine("Incorrect enter of priority. Please try again");
        public static void IntTryParseError() => WriteLine("Incorrect enter of int. Please try again");
        public static void WriteDataToFile(List<WeeklyTask> taskList)
        {
            var path = GetPath();
            using var writeFile = new StreamWriter(path, false, Encoding.Default);
            foreach (var i in taskList)
            {
                writeFile.WriteLine(i.DataToFile());
            }
        }
        public static string GetPath()
        {
            ConsoleWrite("Enter path to file");
            return ReadLine();
        }
         public static WeeklyTask AddTask()
        {
            ConsoleWrite("Write task in format \n\"Name, Date Time, Priority\"\n\"Name, Date Time\"\n\"Name\"");
            return DisassemblyLine(ReadLine());
        }
        public static void DisplayTasks(List<WeeklyTask> taskList)
        {
            DisplayLine();
            foreach (var task in taskList)
            {
                task.DisplayTask();
            }
            DisplayLine();
        }
        public static void DisplayLine()
        {
            ForegroundColor = ConsoleColor.DarkBlue;
            WriteLine("=====================================================================");
        }
        public static int GetID()
        {
            ConsoleWrite("Write ID of task");
            int id;
            while (!int.TryParse(ReadLine(), out id))
            {
                ConsoleWrite("Wrong enter of ID");
            }
            return id;
        }
        public static List<WeeklyTask> ChangeTaskData(List<WeeklyTask> taskList)
        {
            DisplayTasks(taskList);
            var id = GetID();
            var i = 0;
            while (taskList[i].TaskID != id && i < taskList.Count) i++;
            ConsoleWrite("Write changes in format\nParametr value");
            while (true)
            {
                var words = ReadLine().Split(' ');
                switch (words[0].ToLower(System.Globalization.CultureInfo.CurrentCulture)[0])
                {
                    case 'n':
                        taskList[i].TaskName = words[1];
                        return taskList;
                    case 'd':
                        taskList[i].TaskDate = words.Length > 2 ? StringToDate(words[1] + ' ' + words[2]) : StringToDate(words[1]);
                        return taskList;
                    case 'p':
                        taskList[i].TaskPriority = StringToPriority(words[1]);
                        return taskList;
                    default:
                        ConsoleWrite("Uncorrect enter of changes. Please try again");
                        break;
                }
            }
        }
        public static void DisplayTasksWithFilter(List<WeeklyTask> taskList)
        {
            ConsoleWrite("Enter filter in format \npriority/date value");
            while (true)
            {
                var words = ReadLine().ToLower(System.Globalization.CultureInfo.CurrentCulture).Split(' ');
                switch (words[0][0])
                {
                    case 'd':
                        if (words.Length > 2)
                        {
                            DisplayTasksFilteredByDate(taskList, words[1] + ' ' + words[2]);
                        }
                        else
                        {
                            DisplayTasksFilteredByDate(taskList, words[1]);
                        }
                        return;
                    case 'p':
                        DisplayTasksFilteredByPriority(taskList, words[1]);
                        return;
                    default:
                        ConsoleWrite("Uncorrect enter of filter. Please try again");
                        break;
                }
            }
        }
        public static void DisplayTasksFilteredByDate(List<WeeklyTask> taskList, string word)
        {
            var date = StringToDate(word);
            DisplayLine();
            foreach (var task in taskList)
            {
                if (task.TaskDate >= date)
                {
                    task.DisplayTask();
                }
            }
            DisplayLine();
        }
        public static void DisplayTasksFilteredByPriority(List<WeeklyTask> taskList, string word)
        {
            var priority = StringToPriority(word);
            DisplayLine();
            foreach (var task in taskList)
            {
                if(task.TaskPriority == priority)
                {
                    task.DisplayTask();
                }
            }
            DisplayLine();
        }
        public static void ConsoleWrite(string sentence)
        {
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine(sentence);
            ResetColor();
        }
        public class WeeklyTask
        {
            private static int s_counter;
            public string TaskName { get; set; }
            public DateTime TaskDate { get; set; } = DateTime.Today.AddDays(1);
            public Priority TaskPriority { get; set; } = Priority.Low;
            public int TaskID { get; set; }
            public static bool operator >(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate > task2.TaskDate;
            public static bool operator <(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate < task2.TaskDate;
            public static bool operator ==(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate == task2.TaskDate;
            public static bool operator !=(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate != task2.TaskDate;
            public static bool operator >=(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate >= task2.TaskDate;
            public static bool operator <=(WeeklyTask task1, WeeklyTask task2) => task1.TaskDate <= task2.TaskDate;
            public override int GetHashCode() => TaskID;
            public override string ToString()
            {
                return "ID " + TaskID + ", " + TaskName + ", " + TaskDate + ", " + TaskPriority + ",";
            }
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                var objAsPart = obj as WeeklyTask;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }
            public bool Equals(WeeklyTask other)
            {
                if (other == null) return false;
                return TaskID.Equals(other.TaskID);
            }
            public WeeklyTask(string name)
            {
                TaskID = s_counter++;
                TaskName = name;
            }
            public WeeklyTask(string name, DateTime date)
            {
                TaskID = s_counter++;
                TaskName = name;
                TaskDate = date;
            }
            public WeeklyTask(string name, DateTime date, Priority priority)
            {
                TaskID = s_counter++;
                TaskName = name;
                TaskDate = date;
                TaskPriority = priority;
            }
            public int CompareTo(WeeklyTask other)
            {
                if (this > other) { return 1; }
                else if (this == other) { return 0; }
                else { return -1; }
            }      
            public string DataToFile() => TaskName + "," + TaskDate + "," + TaskPriority;
            public void DisplayTask()
            {
                ForegroundColor = (ConsoleColor)TaskPriority;
                WriteLine(this);
            }
        }
    }
}
