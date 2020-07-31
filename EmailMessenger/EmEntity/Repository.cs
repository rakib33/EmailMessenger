using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace EmEntity
{
    public class Repository<TEntity, TContext,DataType> : IRepository<TEntity,DataType>
       where TEntity :class
       where TContext: DbContext      
       {

       protected TContext Context;
       protected DataType type;

       public Repository(DbContext dbContext)
       {
           Context = dbContext as TContext;          
       }

       public Repository(DbContext dbContext,bool enableEagerLoading)
       {
           Context = dbContext as TContext;
           Context.Configuration.LazyLoadingEnabled = !enableEagerLoading;// set false to enable eager .set true enable lazy
       }

       public virtual TEntity Create() {
         
           return Context.Set<TEntity>().Create();
       }

       public virtual TEntity Create(TEntity entity)
       {
           Context.Set<TEntity>().Add(entity);
           if (SaveChanges())
           {
               return entity;
           }
           else {
               return null;
           }
       }
       public virtual void Delete(DataType id)
       {
           var item = Context.Set<TEntity>().Find(id);
           Context.Set<TEntity>().Remove(item);
       }
       public virtual TEntity Update(TEntity entity)
       {
           //Context.Entry(entity).State =
           return entity;
       }

  

       public virtual void Delete(TEntity entity)
       {
           Context.Set<TEntity>().Remove(entity);
       }

       public virtual TEntity FindById(DataType id)
       {
           return Context.Set<TEntity>().Find(id);
       }

       public List<TEntity> FindAll()
       {
           try
           {
               return Context.Set<TEntity>().ToList();
           }
           catch (Exception ex) {
               throw ex;
           }
       }

       //public virtual void Delete(Expression<Func<TEntity, bool>> where)
       //{
       //    var objects = Context.Set<TEntity>().Where(where).AsEnumerable();
       //    foreach (var item in objects)
       //    {
       //        Context.Set<TEntity>().Remove(item);
       //    }
       //}    

       //public virtual TEntity FindOne(Expression<Func<TEntity, bool>> where = null)
       //{
       //    return FindAll(where).FirstOrDefault();
       //}

       //public IQueryable<T> Set<T>() where T : class
       //{
       //    return Context.Set<T>();
       //}

       //public virtual IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> where = null)
       //{
       //    return null != where ? Context.Set<TEntity>().Where(where) : Context.Set<TEntity>();
       //}

       public virtual bool SaveChanges()
       {
           return 0 < Context.SaveChanges();
       }

       /// <summary>
       /// Releases all resources used by the Entities
       /// </summary>
       public void Dispose()
       {
           if (null != Context)
           {
               Context.Dispose();
           }
       }
    }
}
