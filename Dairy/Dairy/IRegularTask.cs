using System;

namespace Dairy
{
    internal interface IRegularTask: IWeeklyTask
    {
        public DateTime Date { get; }
        public DateTime Time { get; }

    }
}
