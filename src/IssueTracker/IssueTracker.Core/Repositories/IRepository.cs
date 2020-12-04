using System;
using System.Collections.Generic;
using IssueTracker.Core.Entities;

namespace IssueTracker.Core.Repositories
{
    interface IRepository<T> where T : EntityBase
    {
        T Create(T entity);
        T GetById(Guid id);
        IList<T> GetAll();
        T Update(T entity);
        void Delete(T entity);
    }
}
