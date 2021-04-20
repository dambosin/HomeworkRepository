using System;

namespace Dairy
{
    internal class PriorityTask : RegularTask, IPriorityTask
    {
        public Priority TaskPriority { get; private set; }

        public override string ToString() => $"{base.ToString()},{TaskPriority}";

        public PriorityTask(string name, DateTime date, DateTime time, Priority priority) : base(name, date, time) => TaskPriority = priority;
    }
}
