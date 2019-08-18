using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IRepositoryBll<T>
        where T : class
    {
        void Create(T item);
        IEnumerable<T> GetAll();
        T Get(string id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Edit(T item);
        void Delete(string id);
    }
}
