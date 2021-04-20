using System;

namespace Dairy
{
    internal class RegularTask : WeeklyTask, IRegularTask
    {
        public DateTime Date { get; private set; }
        public DateTime Time { get; private set; } = new DateTime(1, 1, 1, 12, 0, 0);

        public override string ToString() => $"{base.ToString()},{Date.ToShortDateString()},{Time.ToShortTimeString()}";

        public override int CompareTo(object obj)
        {
            if (obj is DateTime task)
            {
                return Date.CompareTo(task);
            }
            else if(obj is RegularTask reg)
            {
                return Date.CompareTo(reg.Date);
            }
            else
            {
                throw new("Unable to compare 2 objects");
            }
        }

        public RegularTask(string name, DateTime date) : base(name) => Date = date;
        public RegularTask(string name, DateTime date, DateTime time) : this(name, date) => Time = time;
    }
}
