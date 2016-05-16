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
    public class DepositDetailsService : IDepositDetailsService
    {
        public IDepositDetailsRepository iDepositDetailsRepository = null;

        #region Constructor

        /// <summary>
        /// constuctor to initialize the repository
        /// </summary>
        public DepositDetailsService()
        {
            iDepositDetailsRepository = new DepositDetialsRepository();
        }
        #endregion
    }
}
