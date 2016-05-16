using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.Core.Objects;


namespace PartyFund.DataAccess.Implementation.Abstraction
{
    public  interface IBaseRepository<T> where T : class
    {
        //IQueryable<T> SelectAll();
        //T SelectByID(object id);
        //void Insert(T obj);
        //void Update(T obj);
        //void Delete(object id);
        //void Save();
        IQueryable<T> GetAll(Boolean noTrack = false);
        T GetById(int id);
        T GetById(long id);
        T GetByStringKey(string key);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        void DeleteByStringKey(string key);
        ObjectResult<T> StoredProc<T>(string pName, params ObjectParameter[] pParams);
        ObjectResult<T> StoredQuery<T>(string pName, params ObjectParameter[] pParams);
    }    
}
