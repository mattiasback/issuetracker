using System;
using System.Collections.Generic;
using IssueTracker.Core.Repositories;

namespace IssueTracker.Core.Services.UserService.Impl
{
    class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Guid? AddUser(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IList<UserDto> GetUsers()
        {
            throw new NotImplementedException();
        }

        public UserDto GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}