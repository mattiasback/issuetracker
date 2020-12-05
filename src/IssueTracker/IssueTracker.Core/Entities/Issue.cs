using System;
using System.Collections.Generic;
using IssueTracker.Core.Services.IssueService;

namespace IssueTracker.Core.Entities
{
    class Issue : EntityBase
    {
        public Issue(string title)
        {
            CreatedAt = DateTimeOffset.UtcNow;
            State = IssueState.ToDo;
            Title = title;
        }

        public DateTimeOffset CreatedAt { get; }
        public IssueState State { get; set; }
        public string Title { get; set; }
        public User AssignedUser { get; set; }
        public IList<Comment> Comments { get; set; } = new List<Comment>();
        public IList<StateTransition> StateHistory { get; set; } = new List<StateTransition>();
    }
}
