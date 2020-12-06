using System;
using IssueTracker.Core.Repositories;
using IssueTracker.Core.Services;
using IssueTracker.Core.Services.IssueService;
using IssueTracker.Core.Services.IssueService.Impl;
using IssueTracker.Core.Services.UserService;
using IssueTracker.Core.Services.UserService.Impl;
using IssueTracker.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IssueTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Issue Tracker Engine");

            //Setup DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IIssueService, IssueService>()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<IIssueRepository, IssueRepository>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<ITimeProvider, TimeProvider>()
                .BuildServiceProvider();

            //TODO this could be expanded if a UI is desired, but currently xUnit is used for 
            //testing all operations
            var issueService = serviceProvider.GetService<IIssueService>();
            var issueId = issueService.AddIssue("Issue1");
        }
    }
}
