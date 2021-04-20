using System;

namespace Dairy
{
    internal interface IPriorityTask:IRegularTask
    {
        public Priority TaskPriority { get; }
    }
}
