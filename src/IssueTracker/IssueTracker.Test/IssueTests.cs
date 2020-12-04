using System;
using IssueTracker.Core.Services;
using IssueTracker.Core.Services.Impl;
using Xunit;

namespace IssueTracker.Test
{
    public class IssueTests
    {
        private readonly IIssueService _issueService;
        private readonly UserService _userService;

        public IssueTests()
        {
            _issueService = new IssueService();
            _userService = new UserService();
        }

        [Fact]
        public void TestScenario1()
        {
            var userId = _userService.AddUser("Steve");
            var issueId = _issueService.AddIssue("The app crashes on login.");

            Assert.NotNull(userId);
            Assert.NotNull(issueId);

            _issueService.AssignUser(userId.Value, issueId.Value);
            _issueService.SetIssueState(issueId.Value, IssueState.InProgressState, "I'm on it!");
        }
    }
}
