using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyFund.Services.Services.Abstraction;
using PartyFund.DataAccess.Implementation.Repositories;
using PartyFund.DataAccess.Implementation.Abstraction;
namespace PartyFund.Services.Services
{
    public class TransectionDetailsService : ITransectionDetailsService
    {

        public ITransectionDetailsRepository iTransectionDetailsRepository = null;

        #region Constructor
        /// <summary>
        /// To initialize the TransectionDetailsRepository
        /// </summary>
        public TransectionDetailsService()
        {
            iTransectionDetailsRepository = new TransectionDetailsRepository();

        }
        #endregion
    }
}
