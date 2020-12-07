using System.Linq;
using IssueTracker.Core.Entities;
using IssueTracker.Core.Repositories;
using IssueTracker.Core.Services.IssueService;
using IssueTracker.Core.Services.UserService;
using Xunit;

namespace IssueTracker.Test
{
    public class TestScenarios
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIssueService _issueService;
        private readonly IUserService _userService;

        public TestScenarios()
        {
            _issueRepository = new IssueRepository();
            _userRepository = new UserRepository();
            _issueService = new IssueService(_issueRepository, _userRepository, new FakeTimeProvider());
            _userService = new UserService(_userRepository);
        }

        [Fact]
        public void TestScenario1()
        {
            //ARRANGE
            var name = "Steve";
            var title = "The app crashes on login.";
            var state = IssueStateDto.InProgress;
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
            Assert.Equal(state, (IssueStateDto)issue.State);
            Assert.Equal(comment, issue.Comments.First().Text);
        }

        
    }
}