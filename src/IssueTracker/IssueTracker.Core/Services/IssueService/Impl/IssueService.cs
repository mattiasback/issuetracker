using System;
using System.Collections.Generic;
using IssueTracker.Core.Entities;
using IssueTracker.Core.Repositories;

namespace IssueTracker.Core.Services.IssueService.Impl
{
    class IssueService : IIssueService
    {
        private readonly IIssueRepository _repository;

        public IssueService(IIssueRepository repository)
        {
            _repository = repository;
        }

        public Guid? AddIssue(string title)
        {
            var issue = new Issue()
            {
                Title = title
            };

            var created = _repository.Create(issue);
            return created.Id;
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