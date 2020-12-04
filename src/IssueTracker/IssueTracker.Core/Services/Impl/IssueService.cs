using System;
using System.Collections.Generic;

namespace IssueTracker.Core.Services.Impl
{
    internal class IssueService : IIssueService
    {
        public Guid? AddIssue(string title)
        {
            throw new NotImplementedException();
        }

        public void RemoveIssue(Guid issueId)
        {
            throw new NotImplementedException();
        }

        public void SetIssueState(Guid issueId, IssueState state, string comment = null)
        {
            throw new NotImplementedException();
        }

        public void AssignUser(Guid userId, Guid issueId)
        {
            throw new NotImplementedException();
        }

        public void AddIssueComment(Guid issueId, string comment)
        {
            throw new NotImplementedException();
        }

        public IList<IssueDto> GetIssues(IssueState? state = null, Guid? userId = null, 
            DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
        {
            throw new NotImplementedException();
        }

        public IssueDto GetIssue(Guid issueId)
        {
            throw new NotImplementedException();
        }
    }
}