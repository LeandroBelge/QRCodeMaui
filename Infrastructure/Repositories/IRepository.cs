using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        T Add(T t);
        T UpdateOrAddInContext(T t);
        void AddRange(IEnumerable<T> t);
        void UpdateRange(IEnumerable<T> t);
        T Save(T t);
        void Delete(T t);
        void DeleteRange(IEnumerable<T> t);
        T AddNoDetectChangesWithReturn(T t);
        IEnumerable<T> FindAll();
        IQueryable<T> Query();
        IQueryable<T> QueryLazyLoad();
        IEnumerable<T> FindAllIEnumerable();
        void DeleteRangeSemSaveChanges(IEnumerable<T> t);
        void DeleteSemSaveChanges(T t);
        void SaveChanges();
        T RecuperarPorId(int id);
    }
}
