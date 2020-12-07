using System;
using System.Collections.Generic;
using IssueTracker.Core.Entities;

namespace IssueTracker.Core.Repositories
{
    class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        private readonly Dictionary<Guid, T> _database;

        public RepositoryBase()
        {
            _database = new Dictionary<Guid, T>();
        }

        public T Create(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            if(_database.ContainsKey(entity.Id))
                throw new ArgumentException($"ID:{entity.Id} already exists in database!");

            return _database[entity.Id] = entity;
        }

        public T GetById(Guid id)
        {
            if (!_database.ContainsKey(id))
                throw new ArgumentException($"ID:{id} not found in database!");

            return _database[id];
        }

        public IEnumerable<T> GetAll()
        {
            if (_database.Count == 0)
                return new List<T>();

            return _database.Values;
        }

        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!_database.ContainsKey(entity.Id))
                throw new ArgumentException($"ID:{entity.Id} not found in database!");

            return _database[entity.Id] = entity;
        }

        public void Delete(Guid id)
        {
            if (!_database.ContainsKey(id))
                throw new ArgumentException($"ID:{id} not found in database!");

            _database.Remove(id);
        }
    }
}
