using System;

namespace IssueTracker.Core.Services
{
    interface ITimeProvider
    {
        DateTime GetUtcNow();
    }

    class TimeProvider : ITimeProvider
    {
        public virtual DateTime GetUtcNow() => DateTime.UtcNow;
    }
}
