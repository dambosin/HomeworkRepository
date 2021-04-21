using System;
namespace Dairy
{
    internal interface IWeeklyTask : IComparable
    {
        string Name { get; }
    }
}
