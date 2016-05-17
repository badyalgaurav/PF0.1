using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyFund.DataContracts.DataModel;
using PartyFund.Presentation.UI.Common.ViewModels;
namespace PartyFund.DataAccess.Implementation.Abstraction
{
    //: IBaseRepository<UserDetail>
    public interface IUserDetailsRepository
    {
        IQueryable<UserDetail> GetAllUsers();
        UserDetail GetByID(string id);
        void Insert(UserDetailViewModel obj);
        void Update(UserDetailViewModel obj);
        void Delete(string id);
        void Save();
    }
}
