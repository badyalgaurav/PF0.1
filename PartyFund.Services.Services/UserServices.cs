using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyFund.Services.Services.Abstraction;
using PartyFund.Presentation.UI.Common.ViewModels;
using PartyFund.DataAccess.Implementation.Abstraction;
using PartyFund.DataAccess.Implementation.Repositories;

namespace PartyFund.Services.Services
{
    public class UserServices : IUserServices
    {
        public IUserRepository iUserRepository = null;

        #region Constructor
        /// <summary>
        /// Constructor to initialize UserDetailsRepository
        /// </summary>
        public UserServices()
        {
            iUserRepository = new UserRepository();
        }
        #endregion
        public void Insert(UserDetailViewModel obj)
        {
            iUserRepository.Insert(obj);
        }

    }
}
