using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyFund.Services.Services.Abstraction;
using PartyFund.DataAccess.Implementation.Abstraction;
using PartyFund.DataAccess.Implementation.Repositories;
using PartyFund.DataContracts.DataModel;
using PartyFund.Presentation.UI.Common.ViewModels;
namespace PartyFund.Services.Services
{
   public class UserDetailsServices : IUserDetailsServices
    {

       public IUserDetailsRepository iUserDetailsRepository = null;

       #region Constructor
       /// <summary>
       /// Constructor to initialize UserDetailsRepository
       /// </summary>
       public UserDetailsServices()
       {
           iUserDetailsRepository = new UserDetailsRepository();
       }
       #endregion

       public IQueryable<UserDetail> GetAllUsers()
       {
           return iUserDetailsRepository.GetAllUsers();
       }
       public IQueryable<UserDetail> GetByAdminID(string adminID)
       {
           return iUserDetailsRepository.GetByAdminID(adminID);
       }
     public  UserDetail GetByID(string id)
       {
           return iUserDetailsRepository.GetByID(id);
       }
       public void Insert(UserDetailViewModel obj)
       {
          iUserDetailsRepository.Insert(obj);
       }
       public void Update(UserDetailViewModel obj)
       {
           iUserDetailsRepository.Update(obj);
       }
        public void Delete(string id)
       {
           iUserDetailsRepository.Delete(id);
       }
       public void Save()
        { }
    }
}
