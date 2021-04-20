namespace Dairy
{
    internal interface IWeeklyTask
    {
        public string Name { get; }

        public int CompareTo(object obj);
    }
}
