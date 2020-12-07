using System;

namespace IssueTracker.Core.Services.UserService
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
