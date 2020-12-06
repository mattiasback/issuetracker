using System;
using IssueTracker.Core.Services;

namespace IssueTracker.Test
{
    public class FakeTimeProvider : ITimeProvider
    {
        private DateTime _now;

        public FakeTimeProvider()
        {
            _now = DateTime.UtcNow;
        }

        public DateTime GetUtcNow() => _now;
        public void SetUtcNow(DateTime time) => _now = time;
    }
}