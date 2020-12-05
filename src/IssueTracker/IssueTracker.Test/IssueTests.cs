using System.Linq;
using IssueTracker.Core.Repositories;
using IssueTracker.Core.Services.IssueService;
using IssueTracker.Core.Services.IssueService.Impl;
using IssueTracker.Core.Services.UserService;
using IssueTracker.Core.Services.UserService.Impl;
using IssueTracker.Data.Repositories;
using Xunit;

namespace IssueTracker.Test
{
    public class IssueTests
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIssueService _issueService;
        private readonly IUserService _userService;

        public IssueTests()
        {
            _issueRepository = new IssueRepository();
            _userRepository = new UserRepository();
            _issueService = new IssueService(_issueRepository, _userRepository);
            _userService = new UserService(_userRepository);
        }

        [Fact]
        public void TestScenario1()
        {
            //ASSIGN
            var name = "Steve";
            var title = "The app crashes on login.";
            var state = IssueState.InProgress;
            var comment = "I'm on it!";

            //ACT
            var userId = _userService.AddUser(name);
            var issueId = _issueService.AddIssue(title);
            _issueService.AssignUser(userId, issueId);
            _issueService.SetIssueState(issueId, state, comment);

            //ASSERT
            var issue = _issueRepository.GetById(issueId);
            Assert.Equal(userId, issue.AssignedUser.Id);
            Assert.Equal(name, issue.AssignedUser.Name);
            Assert.Equal(title, issue.Title);
            Assert.Equal(state, issue.State);
            Assert.Equal(comment, issue.Comments.First().Text);
        }

        [Fact]
        public void AddIssue()
        {
            _issueService.AddIssue("Issue 1");
        }
    }
}
