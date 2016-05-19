using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyFund.DataContracts.DataModel;
using PartyFund.DataAccess.Implementation.Abstraction;
using PartyFund.Presentation.UI.Common;
using System.Data.Entity;
using PartyFund.Presentation.UI.Common.ViewModels;

namespace PartyFund.DataAccess.Implementation.Repositories
{
    //BaseRepository<UserDetail>,
  public  class UserDetailsRepository :  IUserDetailsRepository
    {
      //need to create constructor when generic class has to inherit
      //public UserDetailsRepository(DbContext context) : base() {

      //}
      private PartyFundEntities context = null;
 
    public UserDetailsRepository()
    {
        this.context = new PartyFundEntities();
    }

    public UserDetailsRepository(PartyFundEntities db)
    {
        this.context = db;
    }

    public IQueryable<UserDetail> GetAllUsers()
    {
        
        return context.UserDetails ;
    }

    public UserDetail GetByID(string id)
    {
      var  Id=Convert.ToInt16(id);
        return context.UserDetails.Find(Id);
    }

    public void Insert(UserDetailViewModel obj)
    {
        var userDetails = new UserDetail {FirstName = obj.FirstName, MiddleName = obj.MiddleName, LastName = obj.LastName, CompanyName = obj.CompanyName };
        context.UserDetails.Add(userDetails);
    }

    public void Update(UserDetailViewModel obj)
    {
        context.Entry(obj).State = EntityState.Modified;
    }
 
    public void Delete(string id)
    {
        UserDetail existing = context.UserDetails.Find(id);
        existing.IsDeleted = true;
       // db.UserDetails.Remove(existing);
    }
 
    public void Save()
    {
        context.SaveChanges();
    }
    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    }
}
