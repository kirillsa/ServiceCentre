using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IRepositoryBll<T>
        where T : class
    {
        void Create(T item);
        IEnumerable<T> GetAll();
        T Get(int id);
        void Edit(T item);
        void Delete(int id);
    }
}
