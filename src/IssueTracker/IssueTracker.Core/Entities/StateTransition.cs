using System;
using IssueTracker.Core.Services.IssueService;

namespace IssueTracker.Core.Entities
{
    class StateTransition
    {
        public StateTransition(IssueState from, IssueState to, DateTime createdAt)
        {
            From = from;
            To = to;
            CreatedAt = createdAt;
        }

        public DateTime CreatedAt { get; }
        public IssueState From { get; }
        public IssueState To { get; }
    }
}
