using System;
using IssueTracker.Core.Services.IssueService;

namespace IssueTracker.Core.Entities
{
    class Issue : EntityBase
    {
        public Issue()
        {
            CreatedAt = DateTimeOffset.UtcNow;
            State = IssueState.ToDo;
        }

        public string Title { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public IssueState State { get; set; }
    }
}
