using System;
using System.Collections.Generic;
using IssueTracker.Core.Services;

namespace IssueTracker.Core.Services.Impl
{
    internal class UserService : IUserService
    {
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