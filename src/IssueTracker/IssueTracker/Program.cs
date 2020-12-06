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
                .AddIssueTracker()
                .BuildServiceProvider();

            //Example usage
            var example = new ExampleClass(serviceProvider.GetService<IIssueService>());
            var issueId = example.AddIssue("Issue1");
        }

        class ExampleClass
        {
            private readonly IIssueService _service;

            public ExampleClass(IIssueService service)
            {
                _service = service;
            }

            public Guid AddIssue(string title)
            {
                return _service.AddIssue(title);
            }
        }
    }
}
