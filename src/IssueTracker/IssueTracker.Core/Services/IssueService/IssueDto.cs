using System;

namespace IssueTracker.Core.Services.IssueService
{
    public class IssueDto
    {
        public Guid IssueId { get; set; }
        public DateTime CreatedAt { get; set; }
        public IssueStateDto State { get; set; }
        public string Title { get; set; }
        public Guid AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
    }
}
