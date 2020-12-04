using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Core.Entities;
using IssueTracker.Core.Repositories;

namespace IssueTracker.Data.Repositories
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
            return _database[id];
        }

        public IList<T> GetAll()
        {
            if (_database.Count == 0)
                return new List<T>();

            return _database.Values.ToList();
        }

        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!_database.ContainsKey(entity.Id))
                throw new ArgumentException($"ID:{entity.Id} not found in database!");

            return _database[entity.Id] = entity;
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!_database.ContainsKey(entity.Id))
                throw new ArgumentException($"ID:{entity.Id} not found in database!");

            _database.Remove(entity.Id);
        }
    }
}
