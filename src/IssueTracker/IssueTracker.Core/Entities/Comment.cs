using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Core.Entities
{
    class Comment : EntityBase
    {
        public Comment(string text)
        {
            CreatedAt = DateTimeOffset.UtcNow;
            Text = text;
        }

        public string Text { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
