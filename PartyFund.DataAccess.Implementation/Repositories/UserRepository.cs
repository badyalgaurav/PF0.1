using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyFund.DataContracts.DataModel;
using PartyFund.DataAccess.Implementation.Abstraction;
using System.Data.Entity;
using PartyFund.Presentation.UI.Common.ViewModels;
namespace PartyFund.DataAccess.Implementation.Repositories
{
    //BaseRepository<User>,
  public class UserRepository : IUserRepository
    {
     // public UserRepository(DbContext context) : base(context) { }

       private PartyFundEntities context = null;

       public UserRepository()
    {
        this.context = new PartyFundEntities();
    }
       public void Insert(UserDetailViewModel model)
       {
           var test = model.Password;
           var userCredentials = new User { Email = model.Email, Password=  model.Password ,Salt = model.Salt, IsActive= true,UserName = model.UserName, DateCreated= DateTime.Now, UserDetailsID = model.ID};
           context.Users.Add(userCredentials);
           Save();
           // userID is userdetails table's ID
           var userID = userCredentials.ID;
           var userInRole = new UserInRole { UserID =userID, RoleID = 1 };
           context.UserInRoles.Add(userInRole);
           Save();
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
