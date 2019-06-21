using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        void Create(T item);
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Update(T item);
        void Delete(int id);
    }
}