using System;

namespace Dairy
{
    internal interface IPriorityTask : IRegularTask
    {
        Priority TaskPriority { get; }
    }
}
