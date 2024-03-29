﻿using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Core.Entities;
using IssueTracker.Core.Repositories;

namespace IssueTracker.Core.Services.UserService
{
    class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Guid AddUser(string name)
        {
            return _repository.Create(new User(name)).Id;
        }

        public void RemoveUser(Guid userId)
        {
            _repository.Delete(userId);
        }

        public IList<UserDto> GetUsers()
        {
            return _repository.GetAll().Select(u => u.MapToDto()).ToList();
        }

        public UserDto GetUser(Guid userId)
        {
            return _repository.GetById(userId).MapToDto();
        }
    }
}