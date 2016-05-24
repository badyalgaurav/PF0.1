using PartyFund.DataContracts.DataModel;
using PartyFund.Presentation.UI.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PartyFund.DataAccess.Implementation.Abstraction
{
     //: IBaseRepository<User>
    public interface IUserRepository
    {
        void Insert(UserDetailViewModel obj);
        void Save();
    }
}
