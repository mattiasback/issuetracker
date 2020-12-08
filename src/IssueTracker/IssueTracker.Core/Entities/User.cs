namespace IssueTracker.Core.Entities
{
    class User : EntityBase
    {
        public User(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
