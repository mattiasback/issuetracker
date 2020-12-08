using System;

namespace IssueTracker.Core.Entities
{
    class Comment
    {
        public Comment(string text, DateTime createdAt)
        {
            Text = text;
            CreatedAt = createdAt;
        }

        public DateTime CreatedAt { get; }
        public string Text { get; }
    }
}
