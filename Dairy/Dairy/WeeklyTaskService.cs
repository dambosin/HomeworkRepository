using System;
using System.Collections.Generic;
using System.IO;
namespace Dairy
{
    internal class WeeklyTaskService
    {
        private readonly List<IWeeklyTask> _taskList = new();
        public string Path { get; private set; }

        public delegate string ReadInput();
        private readonly ReadInput _readInput;

        public delegate void WriteOutput(string text);
        private readonly WriteOutput _writeText;

        public void Loop()
        {
            string userInput = null;

            while (userInput?.ToLower(default) != "close")
            {
                DisplayCommandList();
                userInput = _readInput();
                CommandHandler(userInput);
            }

            WriteTasksToFile();
        }

        private void CommandHandler(string userInputString)
        {
            if(int.TryParse(userInputString, out var userInputInt))
            {
                switch (userInputInt)
                {
                    case 1:
                        NewTaskHandler();
                        break;
                    case 2:
                        ModifyTaskHandler();
                        break;
                    case 3:
                        DisplayTasks();
                        break;
                    case 4:
                        FilterByDateHandler();
                        break;
                    case 5:
                        FilterByPriorityHandler();
                        break;
                    default:
                        _writeText("Out of range");
                        break;
                }
            }
        }

        private void FilterByPriorityHandler()
        {
            _writeText("Enter priority");
            var priority = Enum.Parse<Priority>(_readInput(), default);

            for (var i = 0; i < _taskList.Count; i++)
            {
                if (_taskList[i] is PriorityTask task && task.TaskPriority == priority)
                {
                    _writeText($"{_taskList[i]}");
                }
            }
        }

        private void FilterByDateHandler()
        {
            _writeText("Enter date");
            var date = DateTime.Parse(_readInput(), default);

            for (var i = 0; i < _taskList.Count; i++)
            {
                if (_taskList[i].CompareTo(date) >= 0)
                {
                    _writeText($"{_taskList[i]}");
                }
            }
        }

        private void DisplayTasks()
        {
            for(var i = 0; i < _taskList.Count; i++)
            {
                _writeText($"{i + 1}. {_taskList[i]}");
            }
            _writeText("");
        }

        private void ModifyTaskHandler()
        {
            DisplayTasks();
            _writeText("Choose the id of task");
            var id = int.Parse(_readInput(),(IFormatProvider)default);
            _taskList.Remove(_taskList[id-1]);
            NewTaskHandler();
            _writeText($"Task {id} was changed");
        }

        private void NewTaskHandler()
        {
            _writeText("Write task in format Name,Date,Time,priority");
            NewTaskHandler(_readInput().Split(','));
        }

        private void NewTaskHandler(string[] parts)
        {
            switch (parts.Length)
            {
                case 2:
                    _taskList.Add(new RegularTask(parts[0],DateTime.Parse(parts[1],default)));
                    break;
                case 3:
                    _taskList.Add(new RegularTask(parts[0],DateTime.Parse(parts[1],default), DateTime.Parse(parts[2],default)));
                    break;
                case 4:
                    _taskList.Add(new PriorityTask(parts[0],DateTime.Parse(parts[1],default), DateTime.Parse(parts[2], default), Enum.Parse<Priority>(parts[3])));
                    break;
                default:
                    _writeText("Wrong task format");
                    break;
            }

            _taskList.Sort((a, b) => a.CompareTo(b));
        }

        private void DisplayCommandList()
        {
            _writeText?.Invoke("Choose the command:\n" +
                "1. Add new task;\n" +
                "2. Modify task;\n" +
                "3. Display tasks;\n" +
                "4. Filter by date;\n" +
                "5. Filter by priority.\n");
        }

        private void ReadTasksFromFile(string path)
        {
            Path = path;
            StreamReader sr = new(path);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                NewTaskHandler(line.Split(','));
            }
        }

        private void WriteTasksToFile()
        {
            StreamWriter sw = new(Path);

            for(var i = 0; i< _taskList.Count; i++)
            {
                sw.WriteLine(_taskList[i]);
            }
        }

        internal WeeklyTaskService(string path) => ReadTasksFromFile(path);
        internal WeeklyTaskService(string path, ReadInput input) : this(path) => _readInput = input;
        internal WeeklyTaskService(string path, ReadInput input, WriteOutput output) : this(path, input) => _writeText = output;
    }
}
