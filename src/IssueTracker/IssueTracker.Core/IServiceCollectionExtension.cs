using IssueTracker.Core.Repositories;
using IssueTracker.Core.Services;
using IssueTracker.Core.Services.IssueService;
using IssueTracker.Core.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace IssueTracker.Core
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddIssueTracker(this IServiceCollection services)
        {
            services.AddScoped<IIssueService, IssueService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IIssueRepository, IssueRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ITimeProvider, TimeProvider>();

            return services;
        }
    }
}
