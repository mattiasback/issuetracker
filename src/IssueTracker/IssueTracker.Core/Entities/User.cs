using System.Collections.Generic;

namespace IssueTracker.Core.Entities
{
    class User : EntityBase
    {
        public string Name { get; set; }
        public IList<Issue> AssignedIssues { get; set; } = new List<Issue>();
    }
}
