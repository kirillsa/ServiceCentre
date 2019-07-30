using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IRepositoryBll<T>
        where T : class
    {
        void Create(T item);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        T Get(string id);
        void Edit(T item);
        void Delete(string id);
    }
}
