using IssueTracker.Core.Entities;
using IssueTracker.Core.Services.IssueService;
using IssueTracker.Core.Services.UserService;

namespace IssueTracker.Core.Services
{
    static class DtoExtensions
    {
        public static UserDto MapToDto(this User user)
        {
            return new UserDto()
            {
                UserId = user.Id,
                Name = user.Name
            };
        }

        public static IssueDto MapToDto(this Issue issue)
        {
            return new IssueDto()
            {
                IssueId = issue.Id,
                State = issue.State.ToString(),
                CreatedAt = issue.CreatedAt,
                Title = issue.Title,
                AssignedUserId = issue.AssignedUser.Id,
                AssignedUserName = issue.AssignedUser.Name
            };
        }
    }
}
