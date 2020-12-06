using System;
using System.Collections.Generic;
using IssueTracker.Core.Services.IssueService;

namespace IssueTracker.Core.Services.UserService
{
    class UserDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
