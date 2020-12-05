using System;
using IssueTracker.Core.Services.IssueService;

namespace IssueTracker.Core.Entities
{
    class StateTransition
    {
        public StateTransition(IssueState from, IssueState to)
        {
            CreatedAt = DateTimeOffset.UtcNow;
            From = from;
            To = To;
        }

        public DateTimeOffset CreatedAt { get; }
        public IssueState From { get; }
        public IssueState To { get; }
    }
}
