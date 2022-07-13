using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly QRCodeDbContext _contexto;

        public Repository(QRCodeDbContext contexto)
        {
            _contexto = contexto;
        }

        public T Add(T t)
        {
            var obj = this._contexto.Set<T>().Add(t);
            var saveChangesResult = this._contexto.SaveChanges();
            return obj.Entity;
        }
        public T UpdateOrAddInContext(T t)
        {
            if (t != null)
            {
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> obj;
                if (!this._contexto.Set<T>().Contains<T>(t))
                {
                    obj = this._contexto.Set<T>().Add(t);
                }
                else obj = this._contexto.Set<T>().Update(t);
                return obj.Entity;
            }
            return null;
        }

        public T AddNoDetectChangesWithReturn(T t)
        {
            try
            {
                this._contexto.ChangeTracker.AutoDetectChangesEnabled = false;
                var obj = this._contexto.Set<T>().Add(t);
                this._contexto.SaveChanges();
                return obj.Entity;
            }
            finally
            {
                this._contexto.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        public void AddRange(IEnumerable<T> t)
        {
            this._contexto.Set<T>().AddRange(t);
            this._contexto.SaveChanges();
        }
        public T Save(T t)
        {
            var obj = this._contexto.Set<T>().Update(t);
            this._contexto.SaveChanges();
            return obj.Entity;
        }
        public void UpdateRange(IEnumerable<T> t)
        {
            var lista = t.ToList();
            foreach (var x in lista)
            {
                Save(x);
            }
        }
        public void Delete(T t)
        {
            this._contexto.Set<T>().Remove(t);
            this._contexto.SaveChanges();

        }
        public void DeleteRange(IEnumerable<T> t)
        {
            this._contexto.Set<T>().RemoveRange(t);
            this._contexto.SaveChanges();
        }

        public void DeleteRangeSemSaveChanges(IEnumerable<T> t)
        {
            this._contexto.Set<T>().RemoveRange(t);
        }

        public void DeleteSemSaveChanges(T t)
        {
            this._contexto.Set<T>().Remove(t);
        }

        public void SaveChanges()
        {
            this._contexto.SaveChanges();
        }

        public IEnumerable<T> FindAll()
        {
            return this._contexto.Set<T>().ToList();
        }
        public T RecuperarPorId(int id)
        {
            return this._contexto.Find<T>(id);
        }

        public IEnumerable<T> FindAllIEnumerable()
        {
            return this._contexto.Set<T>().AsEnumerable();
        }

        public IQueryable<T> Query()
        {
            return this._contexto.Set<T>().AsNoTracking();
        }

        public IQueryable<T> QueryLazyLoad()
        {
            var query =  this._contexto.Set<T>();
            return query;
        }
    }
}
