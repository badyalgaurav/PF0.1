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
  public  class UserDetailsRepository :IUserDetailsRepository,IDisposable
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
      return context.UserDetails.Where(x => x.ID == Id).FirstOrDefault(); ;
    }
    public IOrderedQueryable<GetUsersByAdminID_Result2> GetByAdminID(string adminID)
    {
        var adminId = Convert.ToInt16(adminID);
        var result= context.GetUsersByAdminID(adminId).AsQueryable().OrderBy(x=>x.ID);
       
       // var resul = result.Distinct();
        return result;
    }

    public void Insert(UserDetailViewModel model)
    {
        var test = model.Password;
        var userDetails = new UserDetail {FirstName = model.FirstName, MiddleName = model.MiddleName, LastName = model.LastName, CompanyName = model.CompanyName, Address=model.Address,City=model.City,IsActive = true, DateCreated = DateTime.Now, Desgination=model.Desgination, Department=model.Department,Country = model.Country, MobileNumber = model.MobileNumber, ParentID = model.ParentID};
        context.UserDetails.Add(userDetails);
        Save();
        model.ID = userDetails.ID;
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
    protected virtual  void Dispose(bool disposing)
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
