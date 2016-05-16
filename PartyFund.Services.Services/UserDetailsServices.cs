using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyFund.Services.Services.Abstraction;
using PartyFund.DataAccess.Implementation.Abstraction;
using PartyFund.DataAccess.Implementation.Repositories;
namespace PartyFund.Services.Services
{
   public class UserDetailsServices : IUserServices
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
    }
}
