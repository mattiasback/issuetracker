using System;
using IssueTracker.Core;
using IssueTracker.Core.Services.IssueService;
using Microsoft.Extensions.DependencyInjection;

namespace IssueTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("External consumer using issue tracker library");

            //Setup DI
            var serviceProvider = new ServiceCollection()
                //.AddIssueTracker()
                .BuildServiceProvider();

            //External 
            var issueService = serviceProvider.GetService<IIssueService>();
            var issueId = issueService.AddIssue("Issue1");
        }
    }
}
