using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Core.Entities;
using IssueTracker.Core.Repositories;

namespace IssueTracker.Core.Services.IssueService.Impl
{
    class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;

        public IssueService(IIssueRepository issueRepository, IUserRepository userRepository)
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;
        }

        public Guid AddIssue(string title)
        {
            var issue = new Issue(title);
            return _issueRepository.Create(issue).Id;
        }

        public void RemoveIssue(Guid issueId)
        {
            _issueRepository.Delete(issueId);
        }

        public void SetIssueState(Guid issueId, IssueState state, string comment = null)
        {
            var issue = _issueRepository.GetById(issueId);
            var oldState = issue.State;
            issue.State = state;
            issue.Comments.Add(new Comment(comment));
            issue.StateHistory.Add(new StateTransition(oldState, state));
            _issueRepository.Update(issue);
        }

        public void AssignUser(Guid userId, Guid issueId)
        {
            var issue = _issueRepository.GetById(issueId);
            issue.AssignedUser = _userRepository.GetById(userId);
            _issueRepository.Update(issue);
        }

        public void AddIssueComment(Guid issueId, string comment)
        {
            var issue = _issueRepository.GetById(issueId);
            issue.Comments.Add(new Comment(comment));
            _issueRepository.Update(issue);
        }

        public IEnumerable<IssueDto> GetIssues(IssueState? state = null, Guid? userId = null, 
            DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
        {
            var issues = _issueRepository.GetAll();

            if (userId != null)
                issues = issues.Where(i => i.AssignedUser.Id == userId);

            if (state != null)
                issues = issues.Where(i => i.State == state);

            if (startDate != null)
                issues = issues.Where(i => i.CreatedAt.Date >= startDate.Value.Date);

            if (endDate != null)
                issues = issues.Where(i => i.CreatedAt.Date < endDate.Value.Date);

            return issues.Select(i => i.MapToDto());
        }

        public IssueDto GetIssue(Guid issueId)
        {
            return _issueRepository.GetById(issueId).MapToDto();
        }
    }
}