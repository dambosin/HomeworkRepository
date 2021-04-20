namespace Dairy
{
    internal abstract class WeeklyTask:IWeeklyTask
    {
        public string Name{ get; private set; }

        public override string ToString() => Name;

        public WeeklyTask(string name) => Name = name;

        public abstract int CompareTo(object obj);
    }
}
