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
            _issueService = new IssueService(_issueRepository);
            _userService = new UserService(_userRepository);
        }

        [Fact]
        public void TestScenario1()
        {
            var userId = _userService.AddUser("Steve");
            var issueId = _issueService.AddIssue("The app crashes on login.");

            Assert.NotNull(userId);
            Assert.NotNull(issueId);

            _issueService.AssignUser(userId.Value, issueId.Value);
            _issueService.SetIssueState(issueId.Value, IssueState.InProgress, "I'm on it!");
        }

        [Fact]
        public void AddIssue()
        {
            _issueService.AddIssue("Issue 1");
        }
    }
}
