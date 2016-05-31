using PartyFund.DataContracts.DataModel;
using PartyFund.Presentation.UI.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Services.Services.Abstraction
{
   public interface IUserDetailsServices
    {

        IQueryable<UserDetail> GetAllUsers();
        IQueryable<UserDetail> GetByAdminID(string adminID);
        UserDetail GetByID(string id);
        void Insert(UserDetailViewModel obj);
        void Update(UserDetailViewModel obj);
        void Delete(string id);
        void Save();
    }
}
