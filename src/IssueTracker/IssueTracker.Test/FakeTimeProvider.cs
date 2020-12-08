using System;
using IssueTracker.Core.Services;

namespace IssueTracker.Test
{
    class FakeTimeProvider : TimeProvider
    {
        private DateTime _now;

        public FakeTimeProvider()
        {
            _now = DateTime.UtcNow;
        }

        public override DateTime GetUtcNow() => _now;
        public void SetUtcNow(DateTime time) => _now = time;
    }
}