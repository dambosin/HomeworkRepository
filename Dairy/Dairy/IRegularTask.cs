using System;

namespace Dairy
{
    internal interface IRegularTask : IWeeklyTask, IComparable
    {
        DateTime Date { get; }
        DateTime Time { get; }
    }
}
