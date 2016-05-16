using PartyFund.DataAccess.Implementation.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using PartyFund.DataContracts.DataModel;


namespace PartyFund.DataAccess.Implementation.Repositories
{
    /// <summary>
    /// The EF-dependent, generic repository for data access
    /// </summary>
    /// <typeparam name="T">Type of entity for this Repository.</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private PartyFundEntities db = null;
        private DbSet<T> table = null;
        private bool _disposed;


        public BaseRepository()
        {
            this.db = new PartyFundEntities();
            table = db.Set<T>();
        }

        public BaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DbContext = dbContext;
            table = DbContext.Set<T>();
        }
        //public BaseRepository(DbContext dbContext)
        //{
        //    if (dbContext == null)
        //        throw new ArgumentNullException("dbContext");
        //    DbContext = dbContext;
        //    DbSet = DbContext.Set<T>();
        //}

        protected DbContext DbContext { get; set; }

     //   protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetAll(Boolean noTrack = false)
        {
           // return table.ToList();
            return noTrack ? table.AsNoTracking() : table;
        }

        public virtual T GetById(int id)
        {
            return table.Find(id);
        }

        public virtual T GetById(long id)
        {
            return table.Find(id);
        }

        public virtual T GetByStringKey(string key)
        {

            return table.Find(key);
        }

        public ObjectResult<T> StoredProc<T>(string pName, params ObjectParameter[] pParams)
        {
            return ((IObjectContextAdapter)this.DbContext).ObjectContext.ExecuteFunction<T>(pName, pParams);

        }

        public ObjectResult<T> StoredQuery<T>(string pName, params ObjectParameter[] pParams)
        {
            return ((IObjectContextAdapter)this.DbContext).ObjectContext.ExecuteStoreQuery<T>(pName, pParams);

        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                table.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                table.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                table.Attach(entity);
                table.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        public virtual void DeleteByStringKey(string key)
        {
            var entity = GetByStringKey(key);
            if (entity == null) return;
            Delete(entity);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
        //for disposing after every action
        /// <summary>
        /// May not be needed need to confirm
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    DbContext.Dispose();

            _disposed = true;
        }

    }
}
