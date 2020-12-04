using System;

namespace IssueTracker.Core.Services.IssueService
{
    public class IssueDto
    {
        public Guid IssueId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int State { get; set; }
    }
}
