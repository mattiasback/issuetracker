using IssueTracker.Core.Repositories;
using IssueTracker.Core.Services.UserService;
using IssueTracker.Core.Services.UserService.Impl;
using IssueTracker.Data.Repositories;
using Xunit;

namespace IssueTracker.Test
{
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepository = new UserRepository();
            _userService = new UserService(_userRepository);
        }

        [Fact]
        public void AddUser()
        {
            //ARRANGE
            var name = "Steve";

            //ACT
            var userId = _userService.AddUser(name);

            //ASSERT
            var user = _userRepository.GetById(userId);
            Assert.NotNull(user);
            Assert.Equal(userId, user.Id);
            Assert.Equal(name, user.Name);
        }

        [Fact]
        public void RemoveUser()
        {
            //ARRANGE
            var name = "Steve";
            var userId = _userService.AddUser(name);

            //ACT
            _userService.RemoveUser(userId);

            //ASSERT
            Assert.Empty(_userRepository.GetAll());
        }

        [Fact]
        public void GetUser()
        {
            //ARRANGE
            var name = "Steve";
            var userId = _userService.AddUser(name);

            //ACT
            var user = _userService.GetUser(userId);

            //ASSERT
            Assert.Equal(userId, user.UserId);
            Assert.Equal(name, user.Name);
        }

        [Fact]
        public void GetUsers()
        {
            //ARRANGE
            var name = "Steve";
            var secondName = "John";

            var firstUserId = _userService.AddUser(name);
            var secondUserId = _userService.AddUser(secondName);

            //ACT
            var users = _userService.GetUsers();

            //ASSERT
            Assert.Equal(2, users.Count);
            Assert.Contains(users, u => u.UserId == firstUserId);
            Assert.Contains(users, u => u.UserId == secondUserId);
        }
    }
}
