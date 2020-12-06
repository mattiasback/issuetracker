using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Core.Entities;
using IssueTracker.Core.Repositories;

namespace IssueTracker.Core.Services.IssueService
{
    class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITimeProvider _time;

        public IssueService(IIssueRepository issueRepository, IUserRepository userRepository, ITimeProvider timeProvider)
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;
            _time = timeProvider;
        }

        public Guid AddIssue(string title)
        {
            var issue = new Issue(title, _time.GetUtcNow());
            return _issueRepository.Create(issue).Id;
        }

        public void RemoveIssue(Guid issueId)
        {
            _issueRepository.Delete(issueId);
        }

        public void SetIssueState(Guid issueId, IssueStateDto state, string comment = null)
        {
            var issue = _issueRepository.GetById(issueId);
            var oldState = issue.State;
            issue.State = (IssueState)state;
            issue.Comments.Add(new Comment(comment, _time.GetUtcNow()));
            issue.StateHistory.Add(new StateTransition(oldState, issue.State, _time.GetUtcNow()));
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
            issue.Comments.Add(new Comment(comment, _time.GetUtcNow()));
            _issueRepository.Update(issue);
        }

        public IList<IssueDto> GetIssues(IssueStateDto? state = null, Guid? userId = null, 
            DateTime? startDate = null, DateTime? endDate = null)
        {
            var issues = _issueRepository.GetAll();

            if (userId != null)
                issues = issues.Where(i => i.AssignedUser.Id == userId);

            if (state != null)
                issues = issues.Where(i => i.State == (IssueState)state);

            if (startDate != null)
                issues = issues.Where(i => i.CreatedAt.Date >= startDate.Value.Date);

            if (endDate != null)
                issues = issues.Where(i => i.CreatedAt.Date < endDate.Value.Date);

            return issues.Select(i => i.MapToDto()).ToList();
        }

        public IssueDto GetIssue(Guid issueId)
        {
            return _issueRepository.GetById(issueId).MapToDto();
        }
    }
}