using System;
using System.Collections.Generic;
using IssueTracker.Core.Services.IssueService;

namespace IssueTracker.Core.Entities
{
    class Issue : EntityBase
    {
        public Issue(string title, DateTime createdAt)
        {
            CreatedAt = createdAt;
            Title = title;
            State = IssueState.ToDo;
        }

        public DateTime CreatedAt { get; }
        public string Title { get; }
        public IssueState State { get; set; }
        public User AssignedUser { get; set; }
        public IList<Comment> Comments { get; set; } = new List<Comment>();
        public IList<StateTransition> StateHistory { get; set; } = new List<StateTransition>();
    }
}
