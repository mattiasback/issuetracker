using System.Linq;
using IssueTracker.Core.Entities;
using IssueTracker.Core.Repositories;
using Xunit;

namespace IssueTracker.Test
{
    public class RepositoryTests
    {
        private readonly IIssueRepository _repo;
        private readonly FakeTimeProvider _time;
        private readonly Issue _issue;

        public RepositoryTests()
        {
            _repo = new IssueRepository();
            _time = new FakeTimeProvider();
            _issue = new Issue("Issue1", _time.GetUtcNow());
        }

        [Fact]
        public void Create()
        {
            //ARRANGE

            //ACT
            var created = _repo.Create(_issue);

            //ASSERT
            Assert.Equal(_issue.Id, created.Id);
        }

        [Fact]
        public void GetById()
        {
            //ARRANGE
            _repo.Create(_issue);

            //ACT
            var issue = _repo.GetById(_issue.Id);

            //ASSERT
            Assert.Equal(_issue.Id, issue.Id);
        }

        [Fact]
        public void GetAll()
        {
            //ARRANGE
            _repo.Create(_issue);

            //ACT
            var issues = _repo.GetAll();

            //ASSERT
            Assert.Equal(_issue.Id, issues.First().Id);
        }

        [Fact]
        public void Update()
        {
            //ARRANGE
            _repo.Create(_issue);

            //ACT
            var updated = _repo.Update(_issue);

            //ASSERT
            Assert.Equal(_issue.Id, updated.Id);
        }

        [Fact]
        public void Delete()
        {
            //ARRANGE
            _repo.Create(_issue);

            //ACT
            _repo.Delete(_issue.Id);
            var issues = _repo.GetAll();

            //ASSERT
            Assert.Empty(issues);
        }
    }
}
