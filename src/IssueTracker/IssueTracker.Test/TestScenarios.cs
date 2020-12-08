using System.Linq;
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
        private readonly FakeTimeProvider _time;

        public TestScenarios()
        {
            _time = new FakeTimeProvider();
            _issueRepository = new IssueRepository();
            _userRepository = new UserRepository();
            _issueService = new IssueService(_issueRepository, _userRepository, _time);
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

        [Theory]
        [InlineData(IssueStateDto.ToDo, IssueStateDto.ToDo)]
        [InlineData(IssueStateDto.ToDo, IssueStateDto.InProgress)]
        [InlineData(IssueStateDto.ToDo, IssueStateDto.Done)]
        [InlineData(IssueStateDto.InProgress, IssueStateDto.ToDo)]
        [InlineData(IssueStateDto.InProgress, IssueStateDto.InProgress)]
        [InlineData(IssueStateDto.InProgress, IssueStateDto.Done)]
        [InlineData(IssueStateDto.Done, IssueStateDto.ToDo)]
        [InlineData(IssueStateDto.Done, IssueStateDto.InProgress)]
        [InlineData(IssueStateDto.Done, IssueStateDto.Done)]
        public void WhenTransitioningToAnotherIssueState_ThenAnyStateIsAllowed(IssueStateDto from, IssueStateDto to)
        {
            //ARRANGE
            var issueId = _issueService.AddIssue("Issue1");
            _issueService.SetIssueState(issueId, from);
            
            //ACT
            _issueService.SetIssueState(issueId, to);

            //ASSERT
            var issue = _issueRepository.GetById(issueId);
            Assert.Equal(to, (IssueStateDto)issue.State);
        }

        [Fact]
        public void WhenIssueStateChanges_ThenItIsRecordedWithTimestamp()
        {
            //ARRANGE
            var issueId = _issueService.AddIssue("Issue1");

            //ACT
            _issueService.SetIssueState(issueId, IssueStateDto.Done);

            //ASSERT
            var issue = _issueRepository.GetById(issueId);
            Assert.Equal(IssueStateDto.Done, (IssueStateDto)issue.State);
            Assert.Equal(_time.GetUtcNow(), issue.StateHistory[0].CreatedAt);
        }

        [Fact]
        public void WhenFetchingIssues_ThenIssuesCanBeFilteredByState()
        {
            //ARRANGE
            var issueId1 = _issueService.AddIssue("Issue1");
            var issueId2 = _issueService.AddIssue("Issue2");
            var issueId3 = _issueService.AddIssue("Issue3");
            var state = IssueStateDto.Done;

            _issueService.SetIssueState(issueId3, state);

            //ACT
            var issues = _issueService.GetIssues(state);

            //ASSERT
            Assert.Single(issues);
            Assert.Equal(issueId3, issues[0].IssueId);
            Assert.Equal(state, issues[0].State);
        }

        [Fact]
        public void WhenFetchingIssues_ThenIssuesCanBeFilteredByUser()
        {
            //ARRANGE
            var issueId1 = _issueService.AddIssue("Issue1");
            var issueId2 = _issueService.AddIssue("Issue2");
            var issueId3 = _issueService.AddIssue("Issue3");

            var userId = _userService.AddUser("Steve");
            _issueService.AssignUser(userId, issueId3);

            //ACT
            var issues = _issueService.GetIssues(null, userId);

            //ASSERT
            Assert.Single(issues);
            Assert.Equal(issueId3, issues[0].IssueId);
        }

        [Fact]
        public void WhenFetchingIssues_ThenIssuesCanBeFilteredByStartDate()
        {
            //ARRANGE
            var startDate = _time.GetUtcNow().AddDays(1);

            var issueId1 = _issueService.AddIssue("Issue1");

            _time.SetUtcNow(startDate);
            var issueId2 = _issueService.AddIssue("Issue2");

            _time.SetUtcNow(startDate.AddSeconds(1));
            var issueId3 = _issueService.AddIssue("Issue3");

            //ACT
            var issues = _issueService.GetIssues(null, null, startDate);

            //ASSERT
            Assert.Equal(2, issues.Count);
            Assert.Equal(issueId2, issues[0].IssueId);
            Assert.Equal(issueId3, issues[1].IssueId);
        }

        [Fact]
        public void WhenFetchingIssues_ThenIssuesCanBeFilteredByEndDate()
        {
            //ARRANGE
            var endDate = _time.GetUtcNow().AddDays(1);

            var issueId1 = _issueService.AddIssue("Issue1");

            _time.SetUtcNow(endDate);
            var issueId2 = _issueService.AddIssue("Issue2");

            _time.SetUtcNow(endDate.AddSeconds(1));
            var issueId3 = _issueService.AddIssue("Issue3");

            //ACT
            var issues = _issueService.GetIssues(null, null, null, endDate);

            //ASSERT
            Assert.Single(issues);
            Assert.Equal(issueId1, issues[0].IssueId);
        }        
        
        [Fact]
        public void WhenFetchingIssues_ThenIssuesCanBeFilteredByDateRange()
        {
            //ARRANGE
            var startDate = _time.GetUtcNow().AddDays(1);
            var endDate = startDate.AddSeconds(1);
            
            var issueId1 = _issueService.AddIssue("Issue1");

            _time.SetUtcNow(startDate);
            var issueId2 = _issueService.AddIssue("Issue2");

            _time.SetUtcNow(endDate);
            var issueId3 = _issueService.AddIssue("Issue3");

            //ACT
            var issues = _issueService.GetIssues(null, null, startDate, endDate);

            //ASSERT
            Assert.Single(issues);
            Assert.Equal(issueId2, issues[0].IssueId);
        }

        [Fact]
        public void WhenFetchingIssues_ThenIssuesCanBeFilteredBySpecifyingAllFilters()
        {
            //ARRANGE
            var startDate = _time.GetUtcNow().AddDays(1);
            var endDate = startDate.AddSeconds(1);

            var issueId1 = _issueService.AddIssue("Issue1");

            _time.SetUtcNow(startDate);
            var issueId2 = _issueService.AddIssue("Issue2");

            _time.SetUtcNow(endDate);
            var issueId3 = _issueService.AddIssue("Issue3");

            var state = IssueStateDto.InProgress;
            _issueService.SetIssueState(issueId2, state);
            var userId = _userService.AddUser("Steve");
            _issueService.AssignUser(userId, issueId2);

            //ACT
            var issues = _issueService.GetIssues(state, userId, startDate, endDate);

            //ASSERT
            Assert.Single(issues);
            Assert.Equal(issueId2, issues[0].IssueId);
        }

        [Fact]
        public void WhenFetchingIssues_ThenEmptyListIsReturnedIfAllIssuesFiltered()
        {
            //ARRANGE
            var issueId1 = _issueService.AddIssue("Issue1");
            var issueId2 = _issueService.AddIssue("Issue2"); 
            var issueId3 = _issueService.AddIssue("Issue3");

            //ACT
            var issues = _issueService.GetIssues(IssueStateDto.Done);

            //ASSERT
            Assert.Empty(issues);
        }
    }
}
