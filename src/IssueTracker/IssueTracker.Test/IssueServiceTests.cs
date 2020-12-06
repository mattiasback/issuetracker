using System;
using IssueTracker.Core.Entities;
using IssueTracker.Core.Repositories;
using IssueTracker.Core.Services;
using IssueTracker.Core.Services.IssueService;
using Xunit;

namespace IssueTracker.Test
{
    public class IssueTests
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIssueService _issueService;
        private readonly ITimeProvider _time;

        public IssueTests()
        {
            _issueRepository = new IssueRepository();
            _userRepository = new UserRepository();
            _time = new FakeTimeProvider();
            _issueService = new IssueService(_issueRepository, _userRepository, _time);
        }

        [Fact]
        public void AddIssue()
        {
            //ARRANGE
            var title = "Issue1";

            //ACT
            var issueId = _issueService.AddIssue(title);

            //ASSERT
            var issue = _issueRepository.GetById(issueId);
            Assert.NotNull(issue);
            Assert.Equal(issueId, issue.Id);
            Assert.Equal(IssueState.ToDo, issue.State);
            Assert.Equal(title, issue.Title);
            Assert.Empty(issue.Comments);
            Assert.Empty(issue.StateHistory);
            Assert.Null(issue.AssignedUser);
            Assert.Equal(DateTime.UtcNow.Date, issue.CreatedAt.Date);
        }

        [Fact]
        public void RemoveIssue()
        {
            //ARRANGE
            var title = "Issue1";
            var issueId = _issueService.AddIssue(title);

            //ACT
            _issueService.RemoveIssue(issueId);

            //ASSERT
            Assert.Empty(_issueRepository.GetAll());
        }

        [Fact]
        public void SetIssueState()
        {
            //ARRANGE
            var state = IssueStateDto.InProgress;
            var title = "Issue1";
            var comment = "In progress!";
            var issueId = _issueService.AddIssue(title);

            //ACT
            _issueService.SetIssueState(issueId, state, comment);

            //ASSERT
            var issue = _issueRepository.GetById(issueId);
            Assert.Equal(issueId, issue.Id);
            Assert.Equal(state, (IssueStateDto)issue.State);
            var expectedTransition = new StateTransition(IssueState.ToDo, issue.State, _time.GetUtcNow());
            Assert.Equal(expectedTransition, issue.StateHistory[0]);
            Assert.Equal(comment, issue.Comments[0].Text);
        }

        [Fact]
        public void AssignUser()
        {
            //ARRANGE
            var issueId = _issueService.AddIssue("Issue1");

            //ACT
            _issueService.AssignUser();

            //ASSERT
        }

        [Fact]
        public void AddIssueComment()
        {
            //ARRANGE

            //ACT

            //ASSERT
        }

        [Fact]
        public void GetIssues()
        {
            //ARRANGE

            //ACT

            //ASSERT
        }

        [Fact]
        public void GetIssue()
        {
            //ARRANGE

            //ACT

            //ASSERT
        }
    }
}
